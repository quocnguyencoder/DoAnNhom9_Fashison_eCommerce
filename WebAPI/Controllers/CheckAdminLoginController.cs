using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class CheckAdminLoginController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/ChangeStatus
        public IHttpActionResult GetAdminLogin(string username, string password)
        {
            return Ok(db.sp_CheckAdminLogin(username, password, 0));
        }
    }
}
