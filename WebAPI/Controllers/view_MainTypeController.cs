using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class view_MainTypeController : ApiController
    {
        DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // api/view_MainType
        public IQueryable<view_MainType> GetMainTypes()
        {
            return db.view_MainType;
        }

        // api/view_MainType/1
        public IQueryable<view_MainType> GetMainTypebyID(int id)
        {
            return db.view_MainType.Where(x=> x.ID == id);
        }
    }
}
