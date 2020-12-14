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
using Fashison_eCommerce.ViewModel;
//using Fashison_eCommerce.Models;
using PagedList;

namespace Fashison_eCommerce.Areas.Seller.Controllers
{
    public class ProductsController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        public const int PageSize = 4;
        // GET: Seller/Products
        public ActionResult Index(int?page)
        {

            ViewBag.MaintypeID = new SelectList(db.Main_Type, "ID", "Name");
            ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name");
            

            int pageNumber = (page ?? 1);
            ProductClient CC = new ProductClient();
            int Storeid = CC.Storeid(Convert.ToInt32(Session["userID"]));
            ViewBag.listProducts = CC.findAll(Storeid);
           
            ViewBag.CurrentPage = pageNumber;
            
            return View();


        }
        //public PartialViewResult Getpaging(int? page)
        //{
            
        //    ProductClient CC = new ProductClient();
        //    int Storeid = CC.Storeid(Convert.ToInt32(Session["userID"]));
           
        //    int pageNumber = (page ?? 1);
        //    ViewBag.listProducts = CC.findAll(Storeid).ToPagedList(pageNumber, pageSize);
        //    //return View("Index");
        //    return PartialView("~/Areas/Seller/Views/Products/Search.cshtml");
        //}
        public ActionResult Paginate(int pageNumber)
        {
            ProductClient CC = new ProductClient();
            int Storeid = CC.Storeid(Convert.ToInt32(Session["userID"]));
            ViewBag.listProducts = CC.findAll(Storeid).ToPagedList(pageNumber, PageSize);
            return PartialView("~/Areas/Seller/Views/Products/Search.cshtml");
        }
        public ActionResult Search(string Name,int TypeID,int MinAmount,int MaxAmount)
        {


          
            ProductClient CC = new ProductClient();
            int Storeid = CC.Storeid(Convert.ToInt32(Session["userID"]));
            Find filter = new Find();
            filter.StoreID = Storeid;
            filter.Name = Name;
            filter.TypeID = TypeID;
            filter.QualityMin = MinAmount;
            filter.QualityMax = MaxAmount;
            
           

            ViewBag.listProducts = CC.filter(filter);
            
            return PartialView("~/Areas/Seller/Views/Products/Search.cshtml");
            

            //}

        }
        public JsonResult GetTypeList(int MaintypeID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Product_Type> ListType = db.Product_Type.Where(x => x.MaintypeID == MaintypeID).ToList();
            return Json(ListType, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(ProductViewModel cvm,HttpPostedFileBase file)
        {
           
            //HttpPostedFileBase file = Request.Files[0];
            if (ModelState.IsValid)
            {
                
                if (file != null)
                {
                    string ImageName = Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/images/" + ImageName);

                    // save image in folder
                    file.SaveAs(physicalPath);
                    cvm.product.Pictures = ImageName;
                }
                ProductClient CC = new ProductClient();
                int Storeid = CC.Storeid(Convert.ToInt32(Session["userID"]));
                cvm.product.Store_ID = Storeid;
                CC.Create(cvm.product);
                
            }

            return RedirectToAction("Index");





            

        }

        // GET: Seller/Products/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductClient CC = new ProductClient();
            ProductViewModel CVM = new ProductViewModel();
           CVM.product = CC.find(id);
           ViewBag.TypeID = new SelectList(db.Product_Type, "TypeID", "Name", CVM.product.TypeID);
           ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", CVM.product.BrandID);
            return View("Edit", CVM);
        }

        // POST: Seller/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(ProductViewModel CVM, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                ProductClient CC = new ProductClient();
                if (file == null)
                {
                    CVM.product.Pictures = CC.find(CVM.product.Product_ID).Pictures;
                }
                else
                {
                    string ImageName = Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/images/" + ImageName);

                    // save image in folder
                    file.SaveAs(physicalPath);
                    CVM.product.Pictures = ImageName;
                }
                int Storeid = CC.Storeid(Convert.ToInt32(Session["userID"]));
                CVM.product.Store_ID = Storeid;
                CC.Edit(CVM.product);
            }
            return RedirectToAction("Index"); 
        }
       

        // GET: Seller/Products/Delete/5
        public ActionResult Delete(int id)
        {
            ProductClient CC = new ProductClient();
            CC.Delete(id);
            return RedirectToAction("Index");
        }
        

       
    }
}
