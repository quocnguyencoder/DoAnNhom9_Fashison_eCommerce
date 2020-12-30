using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;

namespace Fashison_eCommerce.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: Admin/Users
        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                var users = db.Users.Include(u => u.Role);
                return View(users.ToList());
            }
            else
            {
                return Redirect("/Admin/Home/Login");
            }
            
        }

        public ActionResult ResetPassword(int id)
        {
            if (ModelState.IsValid)
            {
                db.ResetPassword(id);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult Details(int id)
        {
            User user = new User();
            user = db.Users.Find(id);
            ViewBag.user = user;
            ViewBag.store = db.Stores.Where(x => x.UserID == id).FirstOrDefault();
            ViewBag.ShopProduct = db.sp_ListProductOfShop(id);
            ViewBag.product = db.Products.Where(y => y.Store_ID == id).ToList();
            return View(user);
        }
        
        public ActionResult OrderDetail(int userid)
        {
            ViewBag.user = db.Users.Find(userid);
            ViewBag.store = db.Stores.Where(x => x.UserID == userid).FirstOrDefault();
            //ViewBag.orderdetail = db.ListOrderDetailUser(orderid);
            return PartialView("_OrderDetail");
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "Rolename");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            string username = form["Username"].ToString();
            string email = form["Email"].ToString();
            string password = form["Password"].ToString();
            string name = form["Name"].ToString();
            string address = form["Address"].ToString();
            string gender = form["Gender"].ToString();
            string phone = form["Phone"].ToString();
            DateTime birthday = Convert.ToDateTime(form["Birthday"]);
            int role = Convert.ToInt32(form["RoleID"]);
            User newUser = new User() { Username=username, Email=email, Password=password, Name=name, Address=address, Gender=gender, Phone=phone, Birthday=birthday, RoleID=role, Avatar=null};
            db.Users.Add(newUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int id)
        {
            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.user = user;
            DateTime birthday = new DateTime();
            birthday = Convert.ToDateTime(user.Birthday);
            ViewBag.birthday = birthday;
            return PartialView("_UserEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form)
        {
            int id = Convert.ToInt32(form["Id"]);
            string username = form["Username"].ToString();
            string email = form["Email"].ToString();
            string name = form["Name"].ToString();
            string address = form["Address"].ToString();
            string gender = form["Gender"].ToString();
            string phone = form["Phone"].ToString();
            DateTime birthday = Convert.ToDateTime(form["Birthday"]);
            int role = Convert.ToInt32(form["RoleID"]);
            string password = form["Password"].ToString();
            User user = new User() { Id=id, Username = username, Email = email, Password = password, Name = name, Address = address, Gender = gender, Phone = phone, Birthday = birthday, RoleID = role, Avatar = null};
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
