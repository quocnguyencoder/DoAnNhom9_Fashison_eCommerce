using Fashison_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Buyer.Controllers
{
    public class ProductbyTypeController : Controller
    {
        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        // GET: Buyer/ProductbyType/Index?typeid=typeid

        List<view_Product> list = new List<view_Product>();
        public ActionResult Index(int typeid)
        {
            view_ProductClient CC = new view_ProductClient();
            //can san pham cung loai
            ViewBag.ProductsByType = CC.findByType(typeid).OrderByDescending(x => x.Name);
            list = CC.findByType(typeid).OrderByDescending(x => x.Name).ToList();

            //So luong san pham theo loai
            ViewBag.count = CC.findByType(typeid).Count();

            //Lay ten loai con
            ViewBag.typeName = db.Product_Type.Where(x => x.TypeID == typeid).FirstOrDefault();

            //Ten loai chinh
            int mainTypeID = Convert.ToInt32(ViewBag.typeName.MaintypeID);
            ViewBag.MainType = db.Main_Type.Where(x => x.ID == mainTypeID).FirstOrDefault();
            return View("ProductWithType");
        }

        public ActionResult ProductByType(int typeid)
        {
            view_ProductClient CC = new view_ProductClient();
            ViewBag.ProductsByType = CC.findByType(typeid);
            return PartialView("LoadProduct");
        }

        public ActionResult SortProduct()
        {
            ViewBag.ProductsByType = list.OrderByDescending(x => x.Price);
            return PartialView("LoadProduct");
        }
    }
}