using Fashison_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Buyer.Controllers
{
    public class ProductbyTypeController : Controller
    {
        // GET: Buyer/ProductbyType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductByType(int typeid)
        {
            view_ProductClient CC = new view_ProductClient();
            ViewBag.Products = CC.findByType(typeid);
            return View("ProductWithType");
        }
    }
}