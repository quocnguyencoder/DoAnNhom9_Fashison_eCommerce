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
    public class Order_CancelController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Order_Cancel
        public IQueryable<Order_Cancel> GetOrder_Cancel()
        {
            return db.Order_Cancel;
        }

        // GET: api/Order_Cancel/5
        [ResponseType(typeof(Order_Cancel))]
        public IHttpActionResult GetOrder_Cancel(string id)
        {
            Order_Cancel order_Cancel = db.Order_Cancel.Find(id);
            if (order_Cancel == null)
            {
                return NotFound();
            }

            return Ok(order_Cancel);
        }

        // PUT: api/Order_Cancel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder_Cancel(string id, Order_Cancel order_Cancel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order_Cancel.Order_ID)
            {
                return BadRequest();
            }

            db.Entry(order_Cancel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_CancelExists(id))
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

        // POST: api/Order_Cancel
        [ResponseType(typeof(Order_Cancel))]
        public IHttpActionResult PostOrder_Cancel(Order_Cancel order_Cancel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order_Cancel.Add(order_Cancel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Order_CancelExists(order_Cancel.Order_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order_Cancel.Order_ID }, order_Cancel);
        }

        // DELETE: api/Order_Cancel/5
        [ResponseType(typeof(Order_Cancel))]
        public IHttpActionResult DeleteOrder_Cancel(string id)
        {
            Order_Cancel order_Cancel = db.Order_Cancel.Find(id);
            if (order_Cancel == null)
            {
                return NotFound();
            }

            db.Order_Cancel.Remove(order_Cancel);
            db.SaveChanges();

            return Ok(order_Cancel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Order_CancelExists(string id)
        {
            return db.Order_Cancel.Count(e => e.Order_ID == id) > 0;
        }
    }
}