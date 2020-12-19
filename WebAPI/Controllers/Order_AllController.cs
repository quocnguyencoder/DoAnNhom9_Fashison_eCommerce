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
    public class Order_AllController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Order_All
        public IHttpActionResult GetOrder_All(int userID, int status)
        {
            Store store = (from s in db.Stores where s.UserID == userID select s).FirstOrDefault();
            return Ok(db.sp_View_Orders(store.Store_ID,status));
        }

        // GET: api/Order_All/5
        [ResponseType(typeof(Order_All))]
        public IHttpActionResult GetOrder_All(string id)
        {
            Order_All order_All = db.Order_All.Find(id);
            if (order_All == null)
            {
                return NotFound();
            }

            return Ok(order_All);
        }

        // PUT: api/Order_All/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder_All(string id, Order_All order_All)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order_All.Order_ID)
            {
                return BadRequest();
            }

            db.Entry(order_All).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_AllExists(id))
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

        // POST: api/Order_All
        [ResponseType(typeof(Order_All))]
        public IHttpActionResult PostOrder_All(Order_All order_All)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order_All.Add(order_All);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Order_AllExists(order_All.Order_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order_All.Order_ID }, order_All);
        }

        // DELETE: api/Order_All/5
        [ResponseType(typeof(Order_All))]
        public IHttpActionResult DeleteOrder_All(string id)
        {
            Order_All order_All = db.Order_All.Find(id);
            if (order_All == null)
            {
                return NotFound();
            }

            db.Order_All.Remove(order_All);
            db.SaveChanges();

            return Ok(order_All);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Order_AllExists(string id)
        {
            return db.Order_All.Count(e => e.Order_ID == id) > 0;
        }
    }
}