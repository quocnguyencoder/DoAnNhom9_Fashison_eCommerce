using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;

namespace Fashison_eCommerce.Areas.Shipper.Controllers
{
    public class ShipperController : Controller
    {
        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        // GET: Shipper/Shipper
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            var listOrder = db.DetailOrder(2);
            ViewBag.listOrder = listOrder.ToList<DetailOrder_Result>();
            return View();
        }
        public ActionResult OrderDetail(String id)
        {
            return View();
        }
    }
}