using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;

namespace Fashison_eCommerce.Controllers
{
    public class AccountController : Controller
    {
        MY_DB mydb = new MY_DB();
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

                //uid = "1";
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
        [HttpPost]
        public ActionResult VerifyEmail()
        {
            string email = Request["email"];
            ViewBag.email = email;
            // Cau hinh thong tin gmail
            var mail = new SmtpClient("smtp.gmail.com", 25)
            {
                Credentials = new NetworkCredential("quocnguyenyeutu@gmail.com", "01885722611"),
                EnableSsl = true
            };
            // tao gmail
            var message = new MailMessage();
            message.From = new MailAddress("quocnguyenyeutu@gmail.com");
            message.ReplyToList.Add("quocnguyenyeutu@gmail.com");
            message.To.Add(new MailAddress(email));
            message.Subject = "Confirm your email";
            message.Body = "Please confirm your password";

            // gui gmail    
            mail.Send(message);
            return View("TestView");
        }
    }
}