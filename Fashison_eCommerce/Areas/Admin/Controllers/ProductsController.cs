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
    public class ProductsController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                ViewBag.type = db.Main_Type.ToList();
                var products = db.Products.Include(p => p.Brand).Include(p => p.Store).Include(p => p.Product_Type);
                ViewBag.product = products.ToList();
                ViewBag.shop = db.Stores.ToList();
                ViewBag.brand = db.Brands.ToList();
                return View();
            }
            else
            {
                return Redirect("/Admin/Home/Login");
            }
            
        }


        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName");
            ViewBag.Store_ID = new SelectList(db.Stores, "Store_ID", "Address");
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            int id = Convert.ToInt32(form["Id"]);
            string name = form["name"].ToString();
            int price = Convert.ToInt32(form["Price"]);
            int amount = Convert.ToInt32(form["Amount"]);
            int type = Convert.ToInt32(form["Type"]);
            int store = Convert.ToInt32(form["Store"]);
            int brand = Convert.ToInt32(form["Brand"]);
            string des = form["Description"].ToString();
            Product product = new Product() { Product_ID=id, Name = name, Price = price, Amount = amount, TypeID = type, Store_ID = store, BrandID = brand, Decription = des };
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            Product product = db.Products.Find(id);
            ViewBag.product = product;
            ViewBag.TypeID = db.Main_Type.Find(product.Product_Type.Main_Type.ID);
            ViewBag.BrandID = db.Brands.Find(product.BrandID);
            ViewBag.StoreID = db.Stores.Find(product.Store_ID);
            ViewBag.brand = db.Brands;
            ViewBag.store = db.Stores;
            ViewBag.type = db.Main_Type;
            return PartialView("_ProductEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form)
        {
            int id = Convert.ToInt32(form["Id"]);
            string name = form["name"].ToString();
            int price = Convert.ToInt32(form["Price"]);
            int amount = Convert.ToInt32(form["Amount"]);
            int type = Convert.ToInt32(form["Type"]);
            int store = Convert.ToInt32(form["Store"]);
            int brand = Convert.ToInt32(form["Brand"]);
            string des = form["Description"].ToString();
            Product product = new Product() { Product_ID = id, Name = name, Price = price, Amount = amount, TypeID = type, Store_ID = store, Pictures = null, BrandID = brand, Decription = des, status=0 };
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // POST: Admin/Products/Delete/5

        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Confirm(int id)
        {
            db.sp_ConfirmProduct_Admin(id);
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
