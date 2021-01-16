using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;

using PagedList;
using Spire.Xls;


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



            ProductsClient CC = new ProductsClient();
            int Storeid = CC.Storeid(Convert.ToInt32(Session["userID"]));
            ViewBag.listProducts = CC.findAll(Storeid);



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
        //public ActionResult Paginate(int pageNumber)
        //{
        //    ProductsClient CC = new ProductsClient();
        //    int Storeid = CC.Storeid(Convert.ToInt32(Session["userID"]));
        //    ViewBag.listProducts = CC.findAll(Storeid).ToPagedList(pageNumber, PageSize);
        //    return PartialView("~/Areas/Seller/Views/Products/Search.cshtml");
        //}
        public ActionResult Search(string Name, int TypeID, int MinAmount, int MaxAmount)
        {



            ProductsClient CC = new ProductsClient();
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
        public ActionResult Create(ProductViewModel cvm, HttpPostedFileBase file)
        {

            //HttpPostedFileBase file = Request.Files[0];
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    string ImageName = Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Product_Images/" + ImageName);

                    // save image in folder
                    file.SaveAs(physicalPath);
                    cvm.product.Pictures = ImageName;
                }
                ProductsClient CC = new ProductsClient();
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
            ProductsClient CC = new ProductsClient();
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

                ProductsClient CC = new ProductsClient();
                if (file == null)
                {
                    CVM.product.Pictures = CC.find(CVM.product.Product_ID).Pictures;
                }
                else
                {
                    string ImageName = Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Product_Images/" + ImageName);

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
            ProductsClient CC = new ProductsClient();
            CC.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void ImportFromExcel(HttpPostedFileBase postedFile)
        {
         
            if (ModelState.IsValid)
            {
                if (postedFile == null || postedFile.ContentLength > (1024 * 1024 * 50))  // 50MB limit  
                {
                    ModelState.AddModelError("postedFile", "Your file is to large. Maximum size allowed is 50MB !");
                }

                else
                {
                    string filePath = string.Empty;
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls": //For Excel 97-03.  
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //For Excel 07 and above.  
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }

                    try
                    {
                        DataTable dt = new DataTable();
                        conString = string.Format(conString, filePath);

                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.  
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();
                                    

                                    //Read Data from First Sheet.  
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();
                                }
                            }
                        }

                        conString = ConfigurationManager.ConnectionStrings["DB_A6A231_DAQLTMDTEntities_SQL"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(conString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name.  
                                sqlBulkCopy.DestinationTableName = "Product";
                                
                                con.Open();
                                sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                                sqlBulkCopy.ColumnMappings.Add("Price", "Price");
                                sqlBulkCopy.ColumnMappings.Add("Amount", "Amount");
                                sqlBulkCopy.ColumnMappings.Add("TypeID", "TypeID");
                                sqlBulkCopy.ColumnMappings.Add("Store_ID", "Store_ID");
                                sqlBulkCopy.ColumnMappings.Add("NamePicture", "Pictures");
                                sqlBulkCopy.ColumnMappings.Add("Decription", "Decription");
                                sqlBulkCopy.ColumnMappings.Add("BrandID", "BrandID");
                                //sqlBulkCopy.ColumnMappings.Add("Status", "status");
                                sqlBulkCopy.WriteToServer(dt);
                                con.Close();
                                Workbook workbook = new Workbook();
                             
                                workbook.LoadFromFile(filePath);
                                Worksheet sheet = workbook.Worksheets[0];
                           for(int i=0;i<dt.Rows.Count;i++)
                                {

                                    ExcelPicture picture = sheet.Pictures[i];
                                    var ImageName = dt.Rows[i][6];
                                    string physicalPath = Server.MapPath("~/Product_Images/" + ImageName);
                                    picture.Picture.Save(physicalPath);
                                }
                                
                              
                            }
                        }
                        
                        Response.Write("<script>alert('Success');location='Index'</script>");
                      
                    }
                
                    catch
                    {
                        Response.Write("<script>alert('Error');location='Index'</script>");
                       
                    }
                  
                }
            }
           
            Response.Write("<script>alert('no files were selected');location='Index'</script>");
         
        }

    }
}