using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;

namespace Fashison_eCommerce.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ProductClient CC = new ProductClient();
            ViewBag.listProducts = CC.findAll();

            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel cvm)
        {
            ProductClient CC = new ProductClient();
            CC.Create(cvm.product);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            ProductClient CC = new ProductClient();
            CC.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel CVM)
        {
            ProductClient CC = new ProductClient();
            CC.Edit(CVM.product);
            return RedirectToAction("Index");
        }
    }
}