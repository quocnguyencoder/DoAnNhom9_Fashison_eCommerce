using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Controllers
{
    public class ManageUserController : Controller
    {
        // GET: ManageUser
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                try
                {
                    string userName = Session["username"].ToString();
                    string pass = Session["pass"].ToString();
                    string connectionString = @"Data Source=DESKTOP-UML28IP\SQLEXPRESS;Initial Catalog=DB_A6A231_DAQLTMDT;User ID=" + userName + ";Password=" + pass ;
                    SqlConnection con = new SqlConnection(connectionString);
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();

                    string query = "select * from view_Users";
                    SqlCommand cmd = new SqlCommand(query, con);
                    ViewBag.Users = cmd.ExecuteReader();
                }
                catch (Exception e)
                {
                    Response.Write("<script>alert('"+e.Message.ToString()+"')</script>");
                    return View("~/Views/Account/Login.cshtml");
                }
            }
            else
            {
                return View("~/Views/Account/Login.cshtml");
            }
            return View("ManageUser");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                string userName = Session["username"].ToString();
                string pass = Session["pass"].ToString();
                string connectionString = @"Data Source=DESKTOP-UML28IP\SQLEXPRESS;Initial Catalog=DB_A6A231_DAQLTMDT;User ID=" + userName + ";Password=" + pass;
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                string query = "delete from view_Users where Id =" +id;
                SqlCommand cmd = new SqlCommand(query, con);
                if(cmd.ExecuteNonQuery()==0)
                {
                    Response.Write("<script>alert('Error: not deleted')</script>");
                }
                query = "select * from view_Users";
                cmd = new SqlCommand(query, con);
                ViewBag.Users = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message.ToString() + "')</script>");
            }
            return PartialView("ManageUser");
        }

    }
}