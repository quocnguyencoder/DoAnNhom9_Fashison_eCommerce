using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;

namespace Fashison_eCommerce.Areas.Buyer.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: Buyer/ProductDetail
        public ActionResult Index()
        {
            return View();
        }

        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // menu loai san pham
        [ChildActionOnly]
        public PartialViewResult ProductType()
        {
            ViewBag.listType = db.Main_Type.ToList();
            GetSubtype();
            return PartialView(ViewBag.listType);   
        }

        public ActionResult Details(int id)
        {
            if (id == default)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            // chi tiet san pham
            ProductClient CC = new ProductClient();
            ViewBag.detail = CC.find(id);

            // thong tin nhan hieu san pham
            int brandID = Convert.ToInt32(CC.find(id).BrandID);
            ViewBag.brand = db.Brands.Where(x => x.BrandID == brandID).FirstOrDefault();

            // Lay thong tin cua hang
            int storeID = Convert.ToInt32(ViewBag.detail.Store_ID);
            Getstore(storeID);

            GetSubtype();

            if (ViewBag.detail == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // Lay thong tin cua hang
        public ActionResult Getstore(int id)
        {
            ViewBag.store = db.Stores.Where(x => x.Store_ID == id).FirstOrDefault();
            return View();
        }

        // Lay thong tin loại con
        public ActionResult GetSubtype()
        {
            ViewBag.subType = db.Product_Type.ToList();
            return View();
        }
    }
}