using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;

namespace Fashison_eCommerce.Controllers
{
    public class UserAPIController : Controller
    {
        // GET: UserAPI
        public ActionResult Index()
        {
            UserClient CC = new UserClient();
            ViewBag.listUsers = CC.findAll();

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(UserViewModel cvm)
        {
            UserClient CC = new UserClient();
            CC.Create(cvm.user);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            UserClient CC = new UserClient();
            CC.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            UserClient CC = new UserClient();
            UserViewModel CVM = new UserViewModel();
            CVM.user = CC.find(id);
            return View("Edit", CVM);
        }
        [HttpPost]
        public ActionResult Edit(UserViewModel CVM)
        {
            UserClient CC = new UserClient();
            CC.Edit(CVM.user);
            return RedirectToAction("Index");
        }
    }
}