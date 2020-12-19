using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Fashison_eCommerce.Models;


namespace Fashison_eCommerce.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account
        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet] // di toi trang login
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
                    if(obj != null)
                    {
                        Session["userID"] = obj.Id.ToString();
                        Session["username"] = obj.Username.ToString();
                        //Lay mat khau người dùng
                        Session["pass"] = user.Password.ToString();
                        Session["avatar"] = obj.Avatar.ToString();
                        //string username = obj.Username.ToString();
                        return RedirectToAction("Index", "MainPage", new { Area = "Buyer" });
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
            return RedirectToAction("Index", "MainPage", new { Area = "Buyer" });
        }
        
        [HttpGet] // di toi trang dang ki
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost] // thuc hien dang ki
        public ActionResult RegisterUser(User user)
        {
            // kiem tra du lieu nhap
            if (ModelState.IsValid)
            {
                using (var _context = new DB_A6A231_DAQLTMDTEntities())
                {
                    try
                    {
                        var obj = (from u in _context.Users where u.Email == user.Email select u).FirstOrDefault();
                        if (obj == null)
                        {
                            try
                            {
                                // goi stored proc de them user vao csdl
                                var sqlParams = new SqlParameter[]
                                {
                                new SqlParameter("@username", user.Username),
                                new SqlParameter("@pass", user.Password),
                                new SqlParameter("@email", user.Email),
                                };
                                _context.Database.ExecuteSqlCommand("dbo.sp_AccountResgister @username, @pass, @email", sqlParams);
                                Response.Write("<script>alert('Registered!')</script>");
                                return View("Login");
                            }
                            catch
                            {
                                return View("Error");
                            }

                        }
                        else
                        {
                            Response.Write("<script>alert('Username or email existed')</script>");
                            return View("Error");
                        }
                    }
                    catch { }
                    
                }
            }
            return View("Error");

        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]  
        public ActionResult VerifyByEmail()// gui email co kem ma xac nhan cho user
        {
            string user_email = Request["email"];
            using (var _context = new DB_A6A231_DAQLTMDTEntities())
            {
                // query id tu email va password de kiem tra dang nhap

                var obj = (from u in _context.Users where u.Email == user_email select u).FirstOrDefault();
                if (obj != null)
                {
                    Session["user_email"] = user_email;
                    ViewBag.email = user_email;
                    string web_email = "laptrinhwebnhom9@gmail.com";
                    // Cau hinh thong tin gmail
                    var mail = new SmtpClient("smtp.gmail.com", 25)
                    {
                        Credentials = new NetworkCredential(web_email, "123asd456qwe"),
                        EnableSsl = true
                    };
                    // tao gmail
                    var message = new MailMessage();
                    message.From = new MailAddress(web_email);
                    message.ReplyToList.Add(web_email);
                    message.To.Add(new MailAddress(user_email));

                    // Create a random 6-digits number for verification code
                    Random random = new Random();
                    int code = random.Next(100000, 999999);
                    ViewBag.code = code;

                    message.Subject = "Your verification code";
                    message.Body = code + " is your LTWeb verification code.";

                    // gui gmail    
                    mail.Send(message);

                    return View("VerificationCode");
                }
                else
                {
                    Response.Write("<script>alert('Email not found')</script>");
                    return View("ForgotPassword");
                }
            }
        }
        // can chinh sua de user ko dung url di vao truc tiep action!!!!
        [HttpPost] // di toi trang doi mat khau
        public ActionResult ChangePasswordPage()
        {
            return View("ChangePassword");
        }
        [HttpPost] // thuc hien doi mat khau nguoi dung
        public ActionResult ChangePassword()
        {
            string password = Request["password"];
            using (var _context = new DB_A6A231_DAQLTMDTEntities())
            {
                try
                {
                    // goi stored proc de doi mat khau user
                    var sqlParams = new SqlParameter[]
                    {
                        new SqlParameter("@pass", password),
                        new SqlParameter("@email", Session["user_email"]),
                    };
                    _context.Database.ExecuteSqlCommand("dbo.sp_AccountChangePassword @email, @pass", sqlParams);
                    Response.Write("<script>alert('Password changed')</script>");
                    Session.Clear();
                    return View("Login");
                }
                catch
                {
                    Response.Write("<script>alert('Password not changed')</script>");
                    return View("Error");
                }
            }
        }
    }
}