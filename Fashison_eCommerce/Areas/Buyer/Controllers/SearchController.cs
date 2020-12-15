using Fashison_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Buyer.Controllers
{
    public class SearchController : Controller
    {
        // GET: Buyer/Search
        public ActionResult Index()
        
        {
            string name = Request.Form["name"];
            int id = Convert.ToInt32(Request.Form["mainType"]);

            Search(name, id);
            return View();
        }

        public ActionResult Search(string name, int typeid)
        {
            ProductClient PC = new ProductClient();
            ViewBag.Product = PC.search(typeid, name);
            ViewBag.Count = PC.search(typeid, name).Count();
            return View();
        }
    }
}