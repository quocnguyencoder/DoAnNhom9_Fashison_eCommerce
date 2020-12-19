using Fashison_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Buyer.Controllers
{
    public class ShopController : Controller
    {

        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        // GET: Buyer/Shop
        public ActionResult Index(int id)
        {
            
            Getstore(id);
            return View("Shop");
        }

        public ActionResult Getstore(int id)
        {
            ViewBag.store = db.Stores.Where(x => x.Store_ID == id).FirstOrDefault();
            return View();
        }

        public PartialViewResult ProductOfStore(int storeid)
        {
            ProductClient PC = new ProductClient();
            ViewBag.prodOfStore = PC.findbyStore(storeid);
            return PartialView("ProductOfStore");
        }


    }
}