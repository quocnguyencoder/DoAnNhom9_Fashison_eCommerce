using Fashison_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Areas.Seller.Controllers
{
    public class AcountController : Controller
    {
        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        // GET: Seller/Acount
        public ActionResult Dashboard()
        {
            if (Session["userID"] != null)
            {
                ProductsClient CC = new ProductsClient();
                int Storeid = CC.Storeid(Convert.ToInt32(Session["userID"]));
                ViewBag.listProducts = CC.findAll(Storeid);
                return View();
            }
               
            else
            {
                return View("Login");
            }

        }
        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]// thuc hien dang nhap
        public ActionResult VerifyLogin(User user)
        {

            // kiem tra du lieu nhap
            if (ModelState.IsValid)
            {
                // truy van csdl 
                using (var _context = new DB_A6A231_DAQLTMDTEntities())
                {
                    // query id tu email va password de kiem tra dang nhap
                    //var obj = (from u in _context.Users where u.Email == user.Email && u.Password == user.Password select u).FirstOrDefault();
                    var obj = db.sp_Login(user.Email, user.Password).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["userID"] = obj.Id.ToString();
                        Session["username"] = obj.Username.ToString();
                        Session["avatar"] = obj.Avatar.ToString();
                        //string username = obj.Username.ToString();
                        return RedirectToAction("Dashboard", "Acount");
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Email or Password')</script>");
                        return View("Error");
                    }
                }
            }
            return View("Error");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login","Acount");
        }
    }
}