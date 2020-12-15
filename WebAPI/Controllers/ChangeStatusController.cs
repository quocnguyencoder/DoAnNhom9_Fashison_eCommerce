using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ChangeStatusController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/ChangeStatus
        public IHttpActionResult ChangeStatus(string id, int status)
        {
            db.ChangeStatus(id, status);
            db.SaveChanges();
            return Ok();
        }
    }
}
