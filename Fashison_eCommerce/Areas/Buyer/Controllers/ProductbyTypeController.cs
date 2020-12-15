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
        // GET: Buyer/ProductbyType

        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        // GET: Buyer/ProductbyType/Index?typeid=typeid

        //List<view_Product> list = new List<view_Product>();
        public ActionResult Index(int typeid)
        {
            ProductClient CC = new ProductClient();
            //can san pham cung loai
            ViewBag.ProductsByType = CC.findbyType(typeid).OrderByDescending(x => x.Name);
            //list = CC.findbyType(typeid).OrderByDescending(x => x.Name).ToList();

            //So luong san pham theo loai
            ViewBag.count = CC.findbyType(typeid).Count();

            //Lay ten loai con
            ViewBag.typeName = db.Product_Type.Where(x => x.TypeID == typeid).FirstOrDefault();

            //Ten loai chinh
            MainTypeClient MC = new MainTypeClient();
            int mainTypeID = Convert.ToInt32(db.Product_Type.Where(x => x.TypeID == typeid).Select(x => x.MaintypeID).FirstOrDefault());
            
            ViewBag.MainType = MC.findbyID(mainTypeID);
            return View("ProductbyType");
        }
    }
}