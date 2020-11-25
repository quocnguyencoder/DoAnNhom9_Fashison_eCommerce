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
            UserCartClient UC = new UserCartClient();
            ViewBag.Cart = UC.LoadCart(Convert.ToInt32(Session["userID"]));
            return View("Cart");
        }
    }
}