using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;

namespace Fashison_eCommerce.Areas.Buyer.Controllers
{
    public class CartCheckoutController : Controller
    {
        // GET: Buyer/CartCheckout
        public ActionResult Index()
        {
            UserCartClient UC = new UserCartClient();
            var cart_Items = UC.LoadCart(Convert.ToInt32(Session["userID"]));
            // get shop_id of item in cart
            List<int> shop_IDs = new List<int>();
            foreach(UserCart item in cart_Items )
            {
                if(!shop_IDs.Any(id => id == item.Store_ID))
                {
                    shop_IDs.Add(item.Store_ID);
                }
            }
            ViewBag.Shops = shop_IDs;
            ViewBag.Items = cart_Items;
            
            BuyerAddressClient buyerAddressClient = new BuyerAddressClient();
            var addressList = buyerAddressClient.find(Convert.ToInt32(Session["userID"]));
            ViewBag.Address = addressList;
            return View();
        }

        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        public ActionResult GetShop(int id)
        {
            ViewBag.shopName = db.Stores.Where(x => x.Store_ID == id).FirstOrDefault().ShopName;
            return PartialView();
        }
        // tao 1 unique id cho order
        public string GenerateOrderID()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            string id = builder.ToString();
            return id;
        }

        public List<int> CheckAvailableItems()
        {
            List<int> outOfOrder = default;
            UserCartClient UC = new UserCartClient();
            var cart_Items = UC.LoadCart(Convert.ToInt32(Session["userID"]));
            ProductClient PC = new ProductClient();
            foreach (var item in cart_Items)
            {
                var check = PC.find(item.ItemID);
                foreach(var i in check)
                {
                    if (i.Amount == 0)
                    {
                        outOfOrder.Add(item.ItemID);
                    }
                }
                
            }
            return outOfOrder;
        }

        public ActionResult GetUserAddress()
        {
            BuyerAddressClient buyerAddressClient = new BuyerAddressClient();
            
            var addressList = buyerAddressClient.find(Convert.ToInt32(Session["userID"]));
            ViewBag.Address = addressList;
            string a = Session["Address_ID"].ToString();
            return PartialView("PartialAddressCheckout1");
        } 

        public ActionResult GetAddress()
        {
            BuyerAddressClient BAC = new BuyerAddressClient();
            ViewBag.Address = BAC.find(Convert.ToInt32(Session["userID"]));
            Session["tempAddress"] = Session["Address_ID"];
            string a = Session["tempAddress"].ToString();
            return PartialView("PartialAddressCheckout");
        }

        [HttpPost]
        public ActionResult Checkout(int address_ID, string shop_List, string delivery_List)
        {
            var check = CheckAvailableItems();
            if(check == null)
            {
                var shop = shop_List.TrimEnd().Split(' ');
                var delivery = delivery_List.TrimEnd().Split(' ');

                var shopAndDelivery = shop.Zip(delivery, (s, d) => new { Shop = s, Delivery = d });

                UserCartClient UC = new UserCartClient();
                var cart_Items = UC.LoadCart(Convert.ToInt32(Session["userID"]));

                foreach (var i in shopAndDelivery)
                {
                    BuyerOrders buyerOrders = new BuyerOrders();
                    string uniqueID = GenerateOrderID();
                    buyerOrders.Order_ID = uniqueID;
                    buyerOrders.User_ID = Convert.ToInt32(Session["userID"]);
                    buyerOrders.Store_ID = Convert.ToInt32(i.Shop);
                    buyerOrders.delivery = Convert.ToInt32(i.Delivery);
                    buyerOrders.payment = "cod";
                    buyerOrders.created_date = DateTime.Now;
                    buyerOrders.decription = "";
                    buyerOrders.status = 1;
                    buyerOrders.Address_ID = address_ID;
                    buyerOrders.Total_Order = 0;
                    BuyerOrdersViewModel bovm = new BuyerOrdersViewModel();
                    bovm.buyerOrders = buyerOrders;
                    BuyerOrdersClient BOC = new BuyerOrdersClient();
                    BOC.Create(bovm.buyerOrders);

                    foreach(var item in cart_Items)
                    {
                        if(item.Store_ID == Convert.ToInt32(i.Shop))
                        {
                            BuyerOrderItems orderItems = new BuyerOrderItems();
                            orderItems.Order_ID = uniqueID;
                            orderItems.Item_ID = item.ItemID;
                            orderItems.price = item.Price;
                            orderItems.quantity = item.Quantity;
                            orderItems.discount = 0;
                            orderItems.total = item.Price * item.Quantity + orderItems.discount;
                            orderItems.status = 1;
                            BuyerOrderItemsViewModel boivm = new BuyerOrderItemsViewModel();
                            boivm.orderItems = orderItems;
                            BuyerOrderItemsClient BOIC = new BuyerOrderItemsClient();
                            BOIC.Create(boivm.orderItems);
                        }
                    }

                }
                return Content("Success!!!");
            }
            return Content("out of order");
        }
        [HttpPost]
        public ActionResult Create(Address add)
        {

            BuyerAddressClient address = new BuyerAddressClient();

            add.User_ID = Convert.ToInt32(Session["userID"]);
            add.default_address = 0;
            address.Create(add);
            ViewBag.Address = address.find(Convert.ToInt32(Session["userID"]));

            return PartialView("PartialAddressCheckout");
        }

        public ActionResult CompleteChangeAddress()
        {
            BuyerAddressClient address = new BuyerAddressClient();
            ViewBag.Address = address.find(Convert.ToInt32(Session["userID"]));
            
            Session["Address_ID"] = Session["tempAddress"];
            var a = Session["Address_ID"];
            return Redirect("Index");
        }

        public ActionResult RadioButton(int id_address)
        {
            BuyerAddressClient address = new BuyerAddressClient();
            ViewBag.Address = address.find(Convert.ToInt32(Session["userID"]));
            Session["tempAddress"] = id_address;
            var a = Session["tempAddress"];
            return PartialView("PartialAddressCheckout");
        }
    }

}