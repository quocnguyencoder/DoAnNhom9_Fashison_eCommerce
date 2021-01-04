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
    public class Main_TypeController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: Admin/Main_Type
        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                ViewBag.type = db.Main_Type.ToList();
                return View();
            }
            else
            {
                return Redirect("/Admin/Home/Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            int id = Convert.ToInt32(form["id"]);
            string name = form["Name"].ToString();
            Main_Type type = new Main_Type() { Name=name };
            db.Main_Type.Add(type);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Main_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            Main_Type type = db.Main_Type.Find(id);
            ViewBag.typeID = type;
            ViewBag.type = db.Main_Type;
            return PartialView("_TypeEdit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form)
        {
            int id = Convert.ToInt32(form["id"]);
            string name = form["Name"].ToString();
            MainType type = new MainType() { ID = id, Name = name };
            db.Entry(type).State = EntityState.Modified;
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
