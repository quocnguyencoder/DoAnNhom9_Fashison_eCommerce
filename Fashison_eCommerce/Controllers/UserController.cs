﻿using Fashison_eCommerce.Models;
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
            return RedirectToAction("UserProfile","User");
        }
        public ActionResult UserProfile()
        {
            //using (var _context = new DB_A6A231_DAQLTMDTEntities())
            //{
            //    int userID = Convert.ToInt32(Session["userID"]);
            //    var obj = (from u in _context.Users where u.Id == userID select u).FirstOrDefault();
            //    ViewBag.Username = obj.Username;
            //    ViewBag.Email = obj.Email;
            //    ViewBag.Name = obj.Name;
            //    ViewBag.Address = obj.Address;
            //    ViewBag.Gender = obj.Gender;
            //    ViewBag.Phone = obj.Phone;
            //    ViewBag.Birthday = obj.Birthday;
            //}
            return View();
        }
        public ActionResult Purchase()
        {
            BuyerOrdersClient BOC = new BuyerOrdersClient();
            ViewBag.OrdersList = BOC.findAll(Convert.ToInt32(Session["userID"]));
            return View();
        }
        public ActionResult PurchaseDetail(string Order_id)
        {
            BuyerOrderItemsClient BOIC = new BuyerOrderItemsClient();
            ViewBag.DetailList = BOIC.find(Order_id);
            return View();
        }
    }
}