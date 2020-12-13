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
            ViewBag.listOrder_All = OA.findAll();

            Order_ConfirmClient OC = new Order_ConfirmClient();
            ViewBag.listOrder_Confirm = OC.findAll();

            Order_CancelClient OCa = new Order_CancelClient();
            ViewBag.listOrder_Cancel = OCa.findAll();

            Order_DeliveringClient OD = new Order_DeliveringClient();
            ViewBag.listOrder_Delivering = OD.findAll();

            Order_ReceiveClient ORE = new Order_ReceiveClient();
            ViewBag.listOrder_Receive = ORE.findAll();

            Order_DeliveredClient ODE = new Order_DeliveredClient();
            ViewBag.listOrder_Delivered = ODE.findAll();

            return View();
        }
        public ActionResult Detail(string id)
        {
            Order_AllClient OA = new Order_AllClient();
            ViewBag.OrderDetail = OA.find(id);
            return View();
        }
        public ActionResult ChangeStatus(string id, int status)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44320/api/");
            HttpResponseMessage respond = hc.PostAsync("ChangeStatus?id=" + id + "&status=" + status,null).Result;

            return Content(id);
        }
    }
}