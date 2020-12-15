using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI;

namespace WebAPI.Controllers
{
    public class StoresController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();
        [HttpGet]

        // GET: api/Stores/5
        [Route("api/Stores/GetStore/{userID}")]
        [ResponseType(typeof(Store))]

        public IHttpActionResult GetStore(int userID)
        {
            Store store = (from s in db.Stores where s.UserID == userID select s).FirstOrDefault();
            if (store == null)
            {
                return NotFound();
            }
            Store store1 = new Store();
            store1.ShopName = store.ShopName;
            store1.Pictures = store.Pictures;
            store1.Coverpics = store.Coverpics;
            store1.Address = store.Address;
            store1.Decription = store.Decription;
            store1.Store_ID = store.Store_ID;
            store1.UserID = store.UserID;
            return Ok(store1);
        }

        // PUT: api/Stores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStore(int id, Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.Store_ID)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(int id)
        {
            return db.Stores.Count(e => e.Store_ID == id) > 0;
        }
    }
}