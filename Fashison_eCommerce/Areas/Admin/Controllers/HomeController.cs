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
                var x = db.CheckAdminLogin(objUser.Email, objUser.Password).Where(r => r.RESULT == 1).FirstOrDefault();
                if (x != null)
                {
                    Session["Email"] = objUser.Email.ToString();
                    Session["Name"] = user.Name.ToString();
                    Session["Password"] = objUser.Password.ToString();
                    return RedirectToAction("AdminDashboard");

                }
            }
            return View(objUser);
        }

        public ActionResult AdminDashboard()
        {
            if (Session["Email"] != null)
            {
                var report = db.ReportAdminIndex().FirstOrDefault();
                ViewBag.report = report;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}