using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin


        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return View("Login");
            }
            else
            {

            }
            return View();
        }


    }
}