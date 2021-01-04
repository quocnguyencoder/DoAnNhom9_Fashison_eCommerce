using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;

namespace Fashison_eCommerce.Areas.Admin.Controllers
{
    public class BrandsController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: Admin/Brands
        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                var brand = db.Brands;
                ViewBag.brand = brand;
                return View();
            }
            else
            {
                return Redirect("/Admin/Home/Login");
            }
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            int id = Convert.ToInt32(form["id"]);
            string name = form["Name"].ToString();
            Brand newBrand = new Brand() { BrandID = id, BrandName = name};
            db.Brands.Add(newBrand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            var brand = db.Brands.Where(x => x.BrandID == id).FirstOrDefault();
            ViewBag.brand = brand;
            return PartialView("_BrandEdit");
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandID,BrandName")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
