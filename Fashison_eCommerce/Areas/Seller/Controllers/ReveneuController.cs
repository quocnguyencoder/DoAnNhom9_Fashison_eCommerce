using ClosedXML.Excel;
using Fashison_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Seller.Controllers
{
    public class ReveneuController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        // GET: Seller/Reveneu
        public ActionResult Index()
        {
            int userID = Convert.ToInt32(Session["userID"]);
            int storeID = (from s in db.Stores where s.UserID == userID select s).FirstOrDefault().Store_ID;
            List<string> name = new List<string>();
            List<int> data = new List<int>();
            for (int i = 0; i <= 23; i++)
            {
                name.Add(i.ToString());
            }

            for (int i=0;i<=DateTime.Now.Hour;i++)
            {
                var id = new SqlParameter("@storeID", storeID);
                var date = new SqlParameter("@date", DateTime.Now.ToString("MM/dd/yyyy"));

                var hour = new SqlParameter("@hour", i);
                int cost = db.Database.SqlQuery<int>("SELECT [dbo].[getDateTotal](@storeID,@date,@hour)", id, date, hour).FirstOrDefault();
                data.Add(cost);

            }
            TempData["Name"] = name;
            TempData["Data"] = data;
            TempData["Date"] = DateTime.Now.ToString("dd/MM/yyyy");
            var Data = string.Join(",", data);

            var Name = string.Join(",", name);


            ViewBag.DateTotals = Data;
            ViewBag.Date = Name;
           
            return View();
        }
        public ActionResult DateChart(DateTime Date)
        {
            int userID = Convert.ToInt32(Session["userID"]);
            int storeID = (from s in db.Stores where s.UserID == userID select s).FirstOrDefault().Store_ID;
            List<string> name = new List<string>();
            List<int> data = new List<int>();

            for (int i = 0; i <= 23; i++)
            {
                name.Add(i.ToString());
            }
            if (Date.Date < DateTime.Now.Date)
            {
                for (int i = 0; i <= 23; i++)
                {
                    var id = new SqlParameter("@storeID", storeID);
                    var date = new SqlParameter("@date", Date);
                    var hour = new SqlParameter("@hour", i);
                    int cost = db.Database.SqlQuery<int>("SELECT [dbo].[getDateTotal](@storeID,@date,@hour)", id, date, hour).FirstOrDefault();
                    data.Add(cost);
                }
                    
            }


            if (Date.Date == DateTime.Now.Date)
            {
                for (int i = 0; i <= DateTime.Now.Hour; i++)
                {
                    var id = new SqlParameter("@storeID", storeID);
                    var date = new SqlParameter("@date", Date.ToString("MM/dd/yyyy"));
                    var hour = new SqlParameter("@hour", i);
                    int cost = db.Database.SqlQuery<int>("SELECT [dbo].[getDateTotal](@storeID,@date,@hour)", id, date, hour).FirstOrDefault();
                    data.Add(cost);
                }
                    
                   
                
            }
            TempData["Name"] = name;
            TempData["Data"] = data;
            TempData["Date"] = Date.ToString("dd/MM/yyyy");
            if (data!=null)
            {
                var Data = string.Join(",", data);
                ViewBag.DateTotals = Data;
            }
            

            var Name = string.Join(",", name);

            
            ViewBag.Date = Name;
           
            return PartialView("~/Areas/Seller/Views/Reveneu/ReveneuChart.cshtml");
        }
        public ActionResult MonthChart(DateTime Date)
        {
            int userID = Convert.ToInt32(Session["userID"]);
            int storeID = (from s in db.Stores where s.UserID == userID select s).FirstOrDefault().Store_ID;
            List<string> name = new List<string>();
            List<int> data = new List<int>();
            int num = DateTime.DaysInMonth(Date.Year, Date.Month);
            for (int i = 1; i <= num; i++)
            {
                name.Add(i.ToString());
            }
            if (Date.Year<DateTime.Now.Year)
            {
                for (int i = 1; i <= num; i++)
                {
                    var id = new SqlParameter("@storeID", storeID);
                    var date = new SqlParameter("@date", Date);

                    var day = new SqlParameter("@day", i);
                    int cost = db.Database.SqlQuery<int>("SELECT [dbo].[getMonthTotal](@storeID,@date,@day)", id, date, day).FirstOrDefault();
                    data.Add(cost);
                }
            }
            if(Date.Year==DateTime.Now.Year)
            {
                if(Date.Month<DateTime.Now.Month)
                {
                    for (int i = 1; i <= num; i++)
                    {
                        var id = new SqlParameter("@storeID", storeID);
                        var date = new SqlParameter("@date", Date);

                        var day = new SqlParameter("@day", i);
                        int cost = db.Database.SqlQuery<int>("SELECT [dbo].[getMonthTotal](@storeID,@date,@day)", id, date, day).FirstOrDefault();
                        data.Add(cost);
                    }
                }
                if(Date.Month==DateTime.Now.Month)
                {
                    for (int i = 1; i <= DateTime.Now.Day; i++)
                    {

                        var id = new SqlParameter("@storeID", storeID);
                        var date = new SqlParameter("@date", Date);

                        var day = new SqlParameter("@day", i);
                        var cost = db.Database.SqlQuery<int>("SELECT [dbo].[getMonthTotal](@storeID,@date,@day)", id, date, day).FirstOrDefault();
                        data.Add(cost);


                    }
                }
            }
            TempData["Name"] = name;
            TempData["Data"] = data;
            TempData["Date"] = Date.Month+"/"+Date.Year;
            if (data != null)
            {
                var Data = string.Join(",", data);
                ViewBag.DateTotals = Data;
            }

            var Name = string.Join(",", name);

            ViewBag.Date = Name;
            
            return PartialView("~/Areas/Seller/Views/Reveneu/ReveneuChart.cshtml");
        }
        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");
            string date = TempData["Date"].ToString();
          
            dt.Columns.AddRange(new DataColumn[2] {new DataColumn("Date/Hour"), new DataColumn("Doanh thu") });
            dt.Rows.Add(date);
            var a = TempData["Name"];
            List<string> Name = (List<string>)TempData["Name"];
            List<int> Data = (List<int>)TempData["Data"];
            for(int i=0;i<Data.Count;i++)
            {
                dt.Rows.Add(Name[i], Data[i]);
            }
            using (XLWorkbook wb = new XLWorkbook()) 
            {
              
                var ws=wb.Worksheets.Add(dt);
                ws.Row(2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                using (MemoryStream stream = new MemoryStream()) 
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Doanhthu.xlsx");
                }
            }
        }

    }
}