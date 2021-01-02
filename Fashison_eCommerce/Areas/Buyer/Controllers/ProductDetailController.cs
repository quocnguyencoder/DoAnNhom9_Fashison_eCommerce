using Fashison_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Buyer.Controllers
{
    public class ProductDetailController : Controller
    {
        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: Buyer/ProductDetail
        public ActionResult Index()
        {
            return View();
        }

        // GET: Buyer/ProductDetail/Details/5
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
            int brandID = Convert.ToInt32(CC.find(id).Select(x => x.BrandID).FirstOrDefault());
            ViewBag.brand = db.Brands.Where(x => x.BrandID == brandID).FirstOrDefault();

            // Lay thong tin cua hang
            int storeID = Convert.ToInt32(CC.find(id).Select(x => x.Store_ID).FirstOrDefault());
            Getstore(storeID);

            GetSubtype();

            if (ViewBag.detail == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        public ActionResult GetSubtype()
        {
            ViewBag.subType = db.Product_Type.ToList();
            return View();
        }

        public ActionResult Getstore(int id)
        {
            ViewBag.store = db.Stores.Where(x => x.Store_ID == id).FirstOrDefault();
            return View();
        }

        public PartialViewResult ProductType()
        {
            MainTypeClient MC = new MainTypeClient();
            ViewBag.listType = MC.findAll();
            GetSubtype();
            return PartialView();
        }

        public ActionResult RelatedProduct(int typeid)
        {
            ProductClient CC = new ProductClient();
            //can san pham cung loai
            ViewBag.ProductsByType = CC.findbyType(typeid).OrderByDescending(x => x.Name);
            return View();
        }

        public PartialViewResult MainType()
        {
            MainTypeClient MC = new MainTypeClient();
            ViewBag.mainType = MC.findAll();
            return PartialView();
        }

        public PartialViewResult ProductOfStore(int storeid)
        {
            ProductClient PC = new ProductClient();
            ViewBag.prodOfStore = PC.findbyStore(storeid);
            return PartialView();
        }

        public ActionResult SearchProduct(int id)
        {

            return View();
        }
    }
}
