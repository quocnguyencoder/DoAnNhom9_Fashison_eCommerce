using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashison_eCommerce.Models
{
    public class AuthorizeController : ActionFilterAttribute
    {
        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int id = 0;
            if(HttpContext.Current.Session["userID"] != null)
            {
                id = Convert.ToInt32(HttpContext.Current.Session["userID"].ToString());
            } 

            var tbus = db.Users.Where(x => x.Id == id).FirstOrDefault();
            //nếu session=null thì trả về trang đăng nhập
            if (tbus == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
            //session != null
            else
            {
                //lấy danh sách quyền của user
                var us_quyen = db.Authorizes.Where(n => n.RoleID == tbus.RoleID);
                //đếm số lượng quyền
                int slquyen = us_quyen.Count();
                //khởi tạo mảng
                string[] listpermission = new string[slquyen];
                int i = 0;
                //lấy danh sách quyền đưa vào mảng
                foreach (var item in us_quyen)
                {
                    listpermission[i] = item.PermissionID;
                    i++;
                }
                //Lấy tên controller và action
                //string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "-" + filterContext.ActionDescriptor.ActionName;

                //Lấy tên Controller
                string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                //nếu tên controler không có trong mảng quyền của user thì trả về trang đăng nhập
                if (!listpermission.Contains(ControllerName))
                {
                    filterContext.Result = new RedirectResult("~/Account/Login");
                }
            }

        }
    }
}