using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;

namespace Fashison_eCommerce.Areas.Seller.Controllers
{
    public class StoresController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: Seller/Stores
        public ActionResult Index()
        {
            StoreClient CC = new StoreClient();
            StoreViewModel CVM = new StoreViewModel();
            CVM.store = CC.find(Convert.ToInt32(Session["userID"]));
            return View(CVM);
        }





        // POST: Seller/Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(StoreViewModel CVM, HttpPostedFileBase file)
        {
            StoreClient CC = new StoreClient();
            StoreViewModel St = new StoreViewModel();
            St.store = CC.find(Convert.ToInt32(Session["userID"]));
            if (file == null)
            {
                CVM.store.Pictures = St.store.Pictures;
            }
            else
            {
                string ImageName = Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Product_Images/" + ImageName);

                // save image in folder
                file.SaveAs(physicalPath);
                CVM.store.Pictures = ImageName;
            }
            CVM.store.Store_ID = St.store.Store_ID;
            CVM.store.UserID = St.store.UserID;
            CC.Edit(CVM.store);

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