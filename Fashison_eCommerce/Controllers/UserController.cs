using Fashison_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserProfile()
        {
            using (var _context = new DB_A6A231_DAQLTMDTEntities())
            {
                // query id tu email va password de kiem tra dang nhap
                int userID = Convert.ToInt32(Session["userID"]);
                var obj = (from u in _context.Users where u.Id == userID select u).FirstOrDefault();
                ViewBag.Username = obj.Username;
                ViewBag.Email = obj.Email;
                ViewBag.Name = obj.Name;
                ViewBag.Address = obj.Address;
                ViewBag.Gender = obj.Gender;
                ViewBag.Phone = obj.Phone;
                ViewBag.Birthday = obj.Birthday;
            }
            return View();
        }
    }
}