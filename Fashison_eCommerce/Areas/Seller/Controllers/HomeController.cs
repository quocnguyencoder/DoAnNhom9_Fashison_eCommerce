using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Seller.Controllers
{
    public class HomeController : Controller
    {
        // GET: Seller/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}