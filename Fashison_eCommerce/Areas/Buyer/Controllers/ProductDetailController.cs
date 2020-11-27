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
        public int idProd;
        // GET: Buyer/ProductDetail
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                ProductClient CC = new ProductClient();
                ViewBag.listProducts = CC.find(id);

            }
            else
            {
                ProductClient CC = new ProductClient();
                ViewBag.listProducts = CC.find(4);
            }

            return View();
        }

        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        [ChildActionOnly]
        public PartialViewResult ProductType()
        {
            ViewBag.listType = db.Main_Type.ToList();
            return PartialView(ViewBag.listType);   
        }

        [ChildActionOnly]
        public ActionResult SubType()
        {
            ViewBag.subType = db.Product_Type.Where(x => x.MaintypeID == 1).ToList();
            return PartialView(ViewBag.subType);
        }   


        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            idProd = id;
            ProductClient CC = new ProductClient();
            ViewBag.detail = CC.find(id);
            int brandID = Convert.ToInt32(CC.find(id).BrandID);
            ViewBag.brand = db.Brands.Where(x => x.BrandID == brandID).FirstOrDefault();

            int storeID = Convert.ToInt32(ViewBag.detail.Store_ID);
            Getstore(storeID);

            if (ViewBag.detail == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        public ActionResult Getstore(int id)
        {
            ViewBag.store = db.Stores.Where(x => x.Store_ID == id).FirstOrDefault();
            return View();
        }
    }
}