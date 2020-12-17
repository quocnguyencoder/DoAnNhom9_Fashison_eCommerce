using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("UserProfile","User");
        }
        public ActionResult UserProfile()
        {
            //using (var _context = new DB_A6A231_DAQLTMDTEntities())
            //{
            //    int userID = Convert.ToInt32(Session["userID"]);
            //    var obj = (from u in _context.Users where u.Id == userID select u).FirstOrDefault();
            //    ViewBag.Username = obj.Username;
            //    ViewBag.Email = obj.Email;
            //    ViewBag.Name = obj.Name;
            //    ViewBag.Address = obj.Address;
            //    ViewBag.Gender = obj.Gender;
            //    ViewBag.Phone = obj.Phone;
            //    ViewBag.Birthday = obj.Birthday;
            //}
            return View();
        }
        public ActionResult Purchase()
        {
            BuyerOrdersClient BOC = new BuyerOrdersClient();
            ViewBag.OrdersList = BOC.findAll(Convert.ToInt32(Session["userID"]));
            return View();
        }
        public string ConvertToStatus(int stt)
        {
            string status = "";
            switch (stt)
            {
                case 0:
                    status = "đã hủy";
                    break;
                case 1:
                    status = "chờ xác nhận";
                    break;
                case 2:
                    status = "chờ lấy hàng";
                    break;
                case 3:
                    status = "đang giao";
                    break;
                case 4:
                    status = "đã giao hàng";
                    break;
            }
            return status;
        }
        public ActionResult PurchaseDetail(string Order_id)
        {
            BuyerOrderItemsClient BOIC = new BuyerOrderItemsClient();
            ViewBag.DetailList = BOIC.find(Order_id);
            return View();
        }

        public ActionResult Sidebar()
        {
            return PartialView();
        }
        public ActionResult Address()
        {
            BuyerAddressClient add = new BuyerAddressClient();
            ViewBag.listAddresses = add.find(Convert.ToInt32(Session["userID"]));

            return View();
        }
        [HttpPost]
    
        public ActionResult Create(Address add)
        {

            BuyerAddressClient address = new BuyerAddressClient();

            add.User_ID = Convert.ToInt32(Session["userID"]);
            add.default_address = 0;
            address.Create(add);
           ViewBag.listAddresses = address.find(Convert.ToInt32(Session["userID"]));

            return PartialView("PartialAddress");



        }
        [HttpPost]
        public ActionResult Edit(Address add)
        {


            BuyerAddressClient address = new BuyerAddressClient();

            add.User_ID = Convert.ToInt32(Session["userID"]);
     
              address.Edit(add);
            ViewBag.listAddresses = address.find(Convert.ToInt32(Session["userID"]));
            return PartialView("PartialAddress");
        }
        public ActionResult Delete(int id)
        {
            BuyerAddressClient address = new BuyerAddressClient();
            address.Delete(id);
            ViewBag.listAddresses = address.find(Convert.ToInt32(Session["userID"]));
            return PartialView("PartialAddress");
        }
    }
}