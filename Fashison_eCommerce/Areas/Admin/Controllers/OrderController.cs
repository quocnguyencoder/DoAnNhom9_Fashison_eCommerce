using Fashison_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var all = db.Orders.ToList();
            ViewBag.listOrder_All = all;
            var confirm = (from o in db.Orders where o.status == 1 select o).ToList();
            ViewBag.listOrder_Confirm = confirm;
            var cancel = (from o in db.Orders where o.status == 0 select o).ToList();
            ViewBag.listOrder_Cancel = cancel;
            var delivering = (from o in db.Orders where o.status == 3 select o).ToList();
            ViewBag.listOrder_Delivering = delivering;
            var receive = (from o in db.Orders where o.status == 2 select o).ToList();
            ViewBag.listOrder_Receive = receive;
            var delivered = (from o in db.Orders where o.status == 4 select o).ToList();
            ViewBag.listOrder_Delivered = delivered;

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
        public ActionResult Detail(string id)
        {
            BuyerOrderItemsClient BOIC = new BuyerOrderItemsClient();
            ViewBag.DetailList = BOIC.find(id);
            return View();
        }
        public ActionResult _UserAddressByID(int id)
        {
            BuyerAddressClient buyerAddressClient = new BuyerAddressClient();
            ViewBag.Address = buyerAddressClient.findByAddressID(id);
            return PartialView();
        }
       
        public ActionResult _UserAddress(int id)
        {
            BuyerAddressClient buyerAddressClient = new BuyerAddressClient();
            ViewBag.Address = buyerAddressClient.findByAddressID(id);
            return PartialView();
        }
    }
}