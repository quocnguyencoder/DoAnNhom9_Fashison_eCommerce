using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;
using System.Net.Http;

namespace Fashison_eCommerce.Areas.Shipper.Controllers
{
    
    public class ShipperController : Controller
    {
        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        // GET: Shipper/Shipper
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            if (Session["userID"] != null)
            {
                int ID = Convert.ToInt32(Session["userID"].ToString());
                var listOrder = db.DetailOrder(ID);
                ViewBag.listOrder = listOrder.ToList<DetailOrder_Result>();
                return View();
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult OrderDetail(String id)
        {
            return View();
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

                        //Lay mat khau người dùng
                        Session["pass"] = user.Password.ToString();

                        if (obj.Avatar != null)
                        {
                            Session["Avatar"] = obj.Avatar.ToString();
                        }
                        else
                        {
                            Session["Avatar"] = "#.png";
                        }
                        //string username = obj.Username.ToString();
                        if(obj.RoleID == 3)
                        {
                            return RedirectToAction("OrderList", "Shipper");
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid Email or Password')</script>");
                            return RedirectToAction("Login", "Shipper");
                        }    
                        
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

        public ActionResult ChangeStatus(string id, int status)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44320/api/");
            HttpResponseMessage respond = hc.PostAsync("ChangeStatus?id=" + id + "&status=" + status, null).Result;

            return Content(id);
        }
    }
}
