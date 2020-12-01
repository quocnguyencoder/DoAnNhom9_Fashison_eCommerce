using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;

namespace Fashison_eCommerce.Areas.Buyer.Controllers
{
    public class UserCartController : Controller
    {
        // GET: Buyer/UserCart
        public ActionResult Index()
        {           
            return View("Cart");
        }
        public ActionResult LoadCart()
        {
            UserCartClient UC = new UserCartClient();
            ViewBag.Cart = UC.LoadCart(Convert.ToInt32(Session["userID"]));
            return PartialView();
        }
        public ActionResult CountCartItem()
        {
            if(Session["userID"] == null)
            {
                ViewBag.Count = 0;
            }
            else
            {
                UserCartClient UC = new UserCartClient();
                var list = UC.LoadCart(Convert.ToInt32(Session["userID"]));
                ViewBag.Count = list.Count();
            } 
            return PartialView();
        }
        public ActionResult CartInfo()
        {
            return PartialView();
        }
        public ActionResult TotalCost()
        {
            UserCartClient UC = new UserCartClient();
            var userCarts = UC.LoadCart(Convert.ToInt32(Session["userID"]));
            float total = 0;
            foreach (var item in userCarts)
            {
                total += item.Price * item.Quantity;
            }
            ViewBag.Total = total;
            return PartialView();
        }
    }
}