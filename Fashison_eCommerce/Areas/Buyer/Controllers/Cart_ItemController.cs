using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;

namespace Fashison_eCommerce.Areas.Buyer.Controllers
{
    
    public class Cart_ItemController : Controller
    {
        // GET: Buyer/Cart_Item
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddToCart(int ProductID)
        {
            Cart_Item cart_Item = new Cart_Item();
            cart_Item.Cart_ID = Convert.ToInt32(Session["userID"]);
            cart_Item.ItemID = ProductID;
            cart_Item.Quantity = 1;
            Cart_ItemViewModel cim = new Cart_ItemViewModel();
            cim.cart_Item = cart_Item;

            Cart_ItemClient CIC = new Cart_ItemClient();
            CIC.Create(cim.cart_Item);
            return Content("");
        }
        [HttpPost]
        public ActionResult UpdateCart(int ProductID,int Quantity)
        {
            Cart_Item cart_Item = new Cart_Item();
            cart_Item.Cart_ID = Convert.ToInt32(Session["userID"]);
            cart_Item.ItemID = ProductID;
            cart_Item.Quantity = Quantity;
            Cart_ItemViewModel cim = new Cart_ItemViewModel();
            cim.cart_Item = cart_Item;

            Cart_ItemClient CIC = new Cart_ItemClient();
            CIC.Edit(cim.cart_Item);
            return Content("");
        }
        [HttpPost]
        public ActionResult DeleteCartItem(int ProductID)
        {
            Cart_ItemClient CIC = new Cart_ItemClient();
            CIC.Delete(ProductID, Convert.ToInt32(Session["userID"]));
            return Content("");
        }
    }
}