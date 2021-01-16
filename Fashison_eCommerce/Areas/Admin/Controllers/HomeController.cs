using Fashison_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();


        // GET: Admin/Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User objUser)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                var id = db.GetIdFromEmail(objUser.Email).FirstOrDefault();
                user = db.Users.Find(Convert.ToInt32(id));
                var x = db.CheckAdminLogin(objUser.Email, objUser.Password).FirstOrDefault();
                if (x == 1)
                {
                    Session["Email"] = objUser.Email.ToString();
                    Session["Name"] = user.Name.ToString();
                    Session["Password"] = objUser.Password.ToString();
                    return RedirectToAction("index");

                }
            }
            return View(objUser);
        }

        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                var report = db.ReportForAdmin().FirstOrDefault();
                ViewBag.report = report;
                var empl = db.Users.Where(x => x.RoleID == 3 || x.RoleID == 4);
                ViewBag.numEmp = empl.Count();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}