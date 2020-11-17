using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;

namespace Fashison_eCommerce.Controllers
{
    public class ProductOfStoreController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: ProductOfStore
        public ActionResult Index()
        {
            //var products = db.Products.Include(p => p.Store).Include(p => p.Product_Type).Include(p => p.Brand);

            var products = db.Products.Where(u => u.Store_ID == 1);
            return View(products.ToList());
        }

        // GET: ProductOfStore/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: ProductOfStore/Create
        public ActionResult Create()
        {
            ViewBag.Store_ID = new SelectList(db.Stores, "Store_ID", "Address");
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name");
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName");
            return View();
        }

        // POST: ProductOfStore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_ID,Name,Price,Amount,TypeID,Store_ID,Pictures,Decription,BrandID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Store_ID = new SelectList(db.Stores, "Store_ID", "Address", product.Store_ID);
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name", product.TypeID);
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", product.BrandID);
            return View(product);
        }

        // GET: ProductOfStore/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Store_ID = new SelectList(db.Stores, "Store_ID", "Address", product.Store_ID);
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name", product.TypeID);
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", product.BrandID);
            return View(product);
        }

        // POST: ProductOfStore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_ID,Name,Price,Amount,TypeID,Store_ID,Pictures,Decription,BrandID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Store_ID = new SelectList(db.Stores, "Store_ID", "Address", product.Store_ID);
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name", product.TypeID);
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", product.BrandID);
            return View(product);
        }

        // GET: ProductOfStore/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ProductOfStore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
