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
        MY_DB mydb = new MY_DB();
        public string GetUserIDbyEmail(string email)
        {
            string uid=default;
            using (var _context = new DA_QLTMDTEntities())
            {
                // query id tu email va password de kiem tra dang nhap
                try
                {
                    var id = (from u in _context.Users where u.Email == email select u).FirstOrDefault();
                    uid = id.ToString();
                }
                catch { }
            }
            return uid;
        }
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(User user)//xac nhan dang nhap
        {
            //mydb.openConnection();
            //hash password..................................
            //SqlCommand command = new SqlCommand("SELECT id FROM Login WHERE username='" + account.Email + "'and password='" + account.Password + "'", mydb.getConnection);
            string uid = null;
            using (var _context = new DA_QLTMDTEntities())
            {
                // query id tu email va password de kiem tra dang nhap
                try
                {
                    var id = (from u in _context.Users where u.Email == user.Email && u.Password == user.Password select u).FirstOrDefault();
                    uid = id.ToString();
                }
                catch { }
            }

            if (uid != null)
            {
                mydb.closeConnection();
                return View("LoginSuccess");
            }
            else
            {
                mydb.closeConnection();
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(User user)
        {
            // lay id cua user tu email
            string uid = null;
            using (var _context = new DA_QLTMDTEntities())
            {              
                try
                {
                    var id = (from u in _context.Users where u.Email == user.Email select u).FirstOrDefault();
                    uid = id.ToString();
                }
                catch { }

                if (uid == null)
                {
                    //Response.Write("<script>alert('Data inserted successfully')</script>");
                    try
                    {
                        mydb.openConnection();
                        SqlCommand command = new SqlCommand("INSERT INTO Users (Name,Email,Password) Values( '"+user.Name+"', '"+ user.Email + "','" + user.Password + "')", mydb.getConnection);
                        command.ExecuteNonQuery();
                        mydb.closeConnection();
                        Response.Write("<script>alert('Data inserted successfully')</script>");
                        return View("LoginSuccess");
                    }
                    catch 
                    {
                        
                    }

                }
                else
                {
                   // Response.Write("<script>alert('Data inserted successfully')</script>");
                    return View("Error");
                }
            }
            return View();
            
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        string user_id;
        [HttpPost]  
        public ActionResult VerifyByEmail()// gui email co kem ma xac nhan cho user
        {
            string user_email = Request["email"];
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
            int code = random.Next(100000,999999);
            ViewBag.code = code;

            message.Subject = "Your verification code";
            message.Body = code+" is your LTWeb verification code.";

            // gui gmail    
            mail.Send(message);
            // save user id
            user_id = GetUserIDbyEmail(user_email);
            return View("VerificationCode");
        }
        [HttpPost]
        public ActionResult ChangePasswordPage()
        {
            return View("ChangePassword");
        }
        [HttpPost]
        public ActionResult ChangePassword()
        {
            if(user_id != null)
            {
                string password = Request["password"];
                try
                {
                    mydb.openConnection();
                    SqlCommand command = new SqlCommand("Update Users Set Password = '"+password+"' where id = "+user_id, mydb.getConnection);
                    command.ExecuteNonQuery();
                    mydb.closeConnection();
                    Response.Write("<script>alert('Password changed')</script>");
                }
                catch
                {

                }
            }
            else
            {
                Response.Write("<script>alert('Email not found')</script>");
            }
            return View("Login");
        }
    }
}