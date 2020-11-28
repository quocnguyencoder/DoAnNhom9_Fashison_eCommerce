using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Seller.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Seller/Orders
        public ActionResult Index()
        {
            OrderClient OC = new OrderClient();
            ViewBag.listOrders = OC.findAll();
            return View();
        }

        // GET: Seller/Orders/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Seller/Orders/Create
        [HttpPost]
        public ActionResult Create(OrderViewModel ovm)
        {
            OrderClient OC = new OrderClient();
            OC.Create(ovm.order);
            return RedirectToAction("Index");
        }

        // GET: Seller/Orders/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            OrderClient OC = new OrderClient();
            OrderViewModel ovm = new OrderViewModel();
            ovm.order = OC.find(id);
            return View("Edit", ovm);
        }
        
        // POST: Seller/Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(OrderViewModel ovm)
        {
            OrderClient OC = new OrderClient();
            OC.Edit(ovm.order);
            return RedirectToAction("Index");
        }

        // GET: Seller/Orders/Delete/5
        public ActionResult Delete(int id)
        {
            OrderClient OC = new OrderClient();
            OC.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
