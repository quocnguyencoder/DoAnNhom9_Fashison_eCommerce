using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;

namespace Fashison_eCommerce.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(BrandViewModel cvm)
        {
            BrandClient CC = new BrandClient();
            CC.Create(cvm.brand);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            BrandClient CC = new BrandClient();
            CC.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            BrandClient CC = new BrandClient();
            BrandViewModel CVM = new BrandViewModel();
            CVM.brand = CC.find(id);
            return View("Edit", CVM);
        }
        [HttpPost]
        public ActionResult Edit(BrandViewModel CVM)
        {
            BrandClient CC = new BrandClient();
            CC.Edit(CVM.brand);
            return RedirectToAction("Index");
        }
    }
}