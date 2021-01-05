using Fashison_eCommerce.Models;
using Fashison_eCommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Controllers
{
    public class UserController : Controller
    {
        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("UserProfile","User");
        }
        public ActionResult UserProfile()
        {
            if (Session["userID"] != null)
            {
                int id = Convert.ToInt32(Session["userID"]);
                ViewBag.User = db.Users.Where(x => x.Id == id).FirstOrDefault();
                DateTime date = Convert.ToDateTime(ViewBag.User.Birthday);
                string day = date.Day.ToString();
                string month = date.Month.ToString();
                if (date.Day < 10)
                {
                    day = "0" + date.Day;
                }
                if (date.Month < 10)
                {
                    month = "0" + date.Month;
                }

                ViewBag.birthday =  day + "-" + month + "-" + date.Year;
            }

            return View();
        }
        public ActionResult Purchase()
        {
            BuyerOrdersClient BOC = new BuyerOrdersClient();
            ViewBag.OrdersList = BOC.findAll(Convert.ToInt32(Session["userID"]));
            return View();
        }
        public string ConvertToStatus(int stt)
        {
            string status = "";
            switch (stt)
            {
                case 0:
                    status = "đã hủy";
                    break;
                case 1:
                    status = "chờ xác nhận";
                    break;
                case 2:
                    status = "chờ lấy hàng";
                    break;
                case 3:
                    status = "đang giao";
                    break;
                case 4:
                    status = "đã giao hàng";
                    break;
                case 5:
                    status = "trả hàng, hoàn tiền";
                    break;
            }
            return status;
        }

        public ActionResult PurchaseDetail(string Order_id)
        {
            BuyerOrderItemsClient BOIC = new BuyerOrderItemsClient();
            ViewBag.DetailList = BOIC.find(Order_id);
            return View();
        }

        public ActionResult Sidebar()
        {
            return PartialView();
        }
        public ActionResult Address()
        {
            BuyerAddressClient add = new BuyerAddressClient();
            ViewBag.listAddresses = add.find(Convert.ToInt32(Session["userID"]));

            return View();
        }
        [HttpPost]
    
        public ActionResult Create(Address add)
        {

            BuyerAddressClient address = new BuyerAddressClient();

            add.User_ID = Convert.ToInt32(Session["userID"]);
            add.default_address = 0;
            address.Create(add);
           ViewBag.listAddresses = address.find(Convert.ToInt32(Session["userID"]));

            return PartialView("PartialAddress");



        }
        [HttpPost]
        public ActionResult Edit(Address add)
        {
            BuyerAddressClient address = new BuyerAddressClient();

            add.User_ID = Convert.ToInt32(Session["userID"]);
            if(add.default_address == 1)
            {
                 Session["Address_ID"] = add.Address_ID;
            }    
     
            address.Edit(add);
            ViewBag.listAddresses = address.find(Convert.ToInt32(Session["userID"]));
            return PartialView("PartialAddress");
        }
        public ActionResult Delete(int id)
        {
            BuyerAddressClient address = new BuyerAddressClient();
            address.Delete(id);
            ViewBag.listAddresses = address.find(Convert.ToInt32(Session["userID"]));
            return PartialView("PartialAddress");
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Session["userID"] != null)
            {
                int id = Convert.ToInt32(Session["userID"]);
                ViewBag.User = db.Users.Where(x => x.Id == id).FirstOrDefault();
                DateTime date = Convert.ToDateTime(ViewBag.User.Birthday);
                string day = date.Day.ToString();
                string month = date.Month.ToString();
                if (date.Day < 10)
                {
                    day = "0" + date.Day;
                }
                if (date.Month < 10)
                {
                    month = "0" + date.Month;
                }
                
                ViewBag.birthday = date.Year + "-" + month + "-" + day;
            }
            return View();
        }
        public ActionResult showEditProfile()
        {
            int id = Convert.ToInt32(Session["userID"]);
            string name = Request["Username"];
            string email = Request["email"];
            string address = Request["address"];
            string gender = Request["Gender"];

            //không thay đổi giới tính
            if (gender == null)
            {
                gender = db.Users.Where(x => x.Id == id).Select(x => x.Gender).FirstOrDefault();
            }
            string phone = Request["phone"];
            DateTime date = Convert.ToDateTime(Request["date"]);
            string avatar = Session["Avatar"].ToString();
            db.sp_EditProfile(id, name, email, address, gender, phone, date, avatar);
            db.SaveChanges();

            //return RedirectToAction("EditProFile");
            if (Session["userID"] != null)
            {
                id = Convert.ToInt32(Session["userID"]);
                ViewBag.User = db.Users.Where(x => x.Id == id).FirstOrDefault();
                date = Convert.ToDateTime(ViewBag.User.Birthday);
                string day = date.Day.ToString();
                string month = date.Month.ToString();
                if (date.Day < 10)
                {
                    day = "0" + date.Day;
                }
                if (date.Month < 10)
                {
                    month = "0" + date.Month;
                }

                ViewBag.birthday = date.Year + "-" + month + "-" + day;
            }
            return PartialView("EditProfile");
        }

        public string Upload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/User_Images/" + file.FileName));
            Session["avatar"] = file.FileName;
            return "/User_Images/" + file.FileName;
        }

        public ActionResult OrderTracking(string order_id)
        {
            //order_id = "9kMqhoZzEw2";
            BuyerOrderItemsClient BOIC = new BuyerOrderItemsClient();
            var DetailList= BOIC.find(order_id);
            ViewBag.DetailList = DetailList;
            List<Order_Tracking> tracking = db.Order_Tracking.Where(t => t.Order_ID == order_id).ToList<Order_Tracking>();
            ViewBag.length = tracking.Count();
            var tracking_list = tracking.Reverse<Order_Tracking>();
            ViewBag.Tracking = tracking_list.ToList<Order_Tracking>();
            
            return View();
        }
        public ActionResult UserAddress(int id)
        {
            BuyerAddressClient buyerAddressClient = new BuyerAddressClient();
            ViewBag.Address = buyerAddressClient.findByAddressID(id);
            return PartialView();
        }

    }
}