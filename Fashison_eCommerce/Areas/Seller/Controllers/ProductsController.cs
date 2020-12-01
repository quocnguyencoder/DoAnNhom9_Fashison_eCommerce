using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;
using PagedList;

namespace Fashison_eCommerce.Areas.Seller.Controllers
{
    public class ProductsController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: Seller/Products
        public ActionResult Index()
        {

            ViewBag.MaintypeID = new SelectList(db.Main_Type, "ID", "Name");
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name");
            //if (Name==null&&TypeID==null&& MinAmount==null && MaxAmount==null)
            //{

            //if (page == null) page = 1;


            //int pageSize = 2;
            //int pageNumber = (page ?? 1);
            using (var context = new DB_A6A231_DAQLTMDTEntities())
            {
                var id = new SqlParameter("@userID", Session["userID"]);
                var result = context.Database
                   .SqlQuery<Product>("getProducts @userID", id)
                   .ToList();
                List<Product> product = new List<Product>();
                for (int i = 0; i < result.Count; i++)
                {
                    product.Add(db.Products.Find(result[i].Product_ID));
                }


                return View(product);
            }


            

        }
        
        public ActionResult Search(string Name, string TypeID, int MinAmount, int MaxAmount)
        {
            
            using (var context = new DB_A6A231_DAQLTMDTEntities())
            {
                var name = new SqlParameter("@Name", Name);
                var type = new SqlParameter("@Type", TypeID);
                var min = new SqlParameter("@QualityMin", MinAmount);
                var max = new SqlParameter("@QualityMax", MaxAmount);
                var result = context.Database
                .SqlQuery<Product>("FindProducts @Name,@Type,@QualityMin,@QualityMax", name, type, min, max)
                .ToList();
                List<Product> product = new List<Product>();
                for (int i = 0; i < result.Count; i++)
                {
                    product.Add(db.Products.Find(result[i].Product_ID));
                }
                //if (page == null) page = 1;


                //int pageSize = 2;
                //int pageNumber = (page ?? 1);
                //product.OrderBy(p => p.Product_ID);
                //return View(product.ToPagedList(pageNumber, pageSize));
                return PartialView("~/Areas/Seller/Views/Products/Search.cshtml", product);
            }


            //}

        }
        public JsonResult GetTypeList(int MaintypeID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Product_Type> ListType = db.Product_Type.Where(x => x.MaintypeID == MaintypeID).ToList();
            return Json(ListType, JsonRequestBehavior.AllowGet);
        }
        // GET: Seller/Products/Details/5
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
       

  
        // GET: Seller/Products/Create
        public ActionResult Create()
        {
    
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name");
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName");
            return View();
        }
        
        // POST: Seller/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_ID,Name,Price,Amount,TypeID,Store_ID,Pictures,Decription,BrandID")] Product product, HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {

                //HttpPostedFileBase file = Request.Files[0];
                if (file != null)
                {
                    string ImageName = Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/images/" + ImageName);

                    // save image in folder
                    file.SaveAs(physicalPath);
                    product.Pictures = ImageName;
                }

                //save new record in database
                //tblA newRecord = new tblA();
                //newRecord.fname = Request.Form["fname"];
                //newRecord.lname = Request.Form["lname"];
                //newRecord.MoviePoster = ImageName;
                //db.tblAs.Add(newRecord);
                //db.SaveChanges();

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name", product.TypeID);
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", product.BrandID);
            return View(product);
        }

        // GET: Seller/Products/Edit/5
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
           
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name", product.TypeID);
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", product.BrandID);
            return View(product);
        }

        // POST: Seller/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_ID,Name,Price,Amount,TypeID,Store_ID,Pictures,Decription,BrandID")] Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string ImageName = Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/images/" + ImageName);

                    // save image in folder
                    file.SaveAs(physicalPath);
                    product.Pictures = ImageName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name", product.TypeID);
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", product.BrandID);
            return View(product);
        }

        // GET: Seller/Products/Delete/5
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

        // POST: Seller/Products/Delete/5
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
