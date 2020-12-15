using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class GetAddressByIDController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/getAddressByID
        public IHttpActionResult getAddressByID(int id)
        {
            return Ok(db.sp_getAddressByID(id));
        }
    }
}
