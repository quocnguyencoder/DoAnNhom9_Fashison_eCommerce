using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Seller.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Seller/Orders
        public ActionResult Index()
        {
            Order_AllClient OA = new Order_AllClient();
            ViewBag.listOrder_All = OA.findAll(Convert.ToInt32(Session["userID"]),-1);

            ViewBag.listOrder_Confirm = OA.findAll(Convert.ToInt32(Session["userID"]), 1);

            ViewBag.listOrder_Cancel = OA.findAll(Convert.ToInt32(Session["userID"]), 0);

            ViewBag.listOrder_Delivering = OA.findAll(Convert.ToInt32(Session["userID"]), 3);

            ViewBag.listOrder_Receive = OA.findAll(Convert.ToInt32(Session["userID"]), 2);

            ViewBag.listOrder_Delivered = OA.findAll(Convert.ToInt32(Session["userID"]), 4);

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
        public ActionResult UserAddressByID(int id)
        {
            BuyerAddressClient buyerAddressClient = new BuyerAddressClient();
            ViewBag.Address = buyerAddressClient.findByAddressID(id);
            return PartialView();
        }
        public ActionResult ChangeStatus(string id, int status)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44320/api/");
            HttpResponseMessage respond = hc.PostAsync("ChangeStatus?id=" + id + "&status=" + status,null).Result;

            return Content(id);
        }
        public ActionResult UserAddress(int id)
        {
            BuyerAddressClient buyerAddressClient = new BuyerAddressClient();
            ViewBag.Address = buyerAddressClient.findByAddressID(id);
            return PartialView();
        }
    }
}