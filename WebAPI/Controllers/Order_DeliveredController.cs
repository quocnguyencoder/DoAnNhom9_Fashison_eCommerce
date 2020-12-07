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
    public class Order_DeliveredController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Order_Delivered
        public IQueryable<Order_Delivered> GetOrder_Delivered()
        {
            return db.Order_Delivered;
        }

        // GET: api/Order_Delivered/5
        [ResponseType(typeof(Order_Delivered))]
        public IHttpActionResult GetOrder_Delivered(string id)
        {
            Order_Delivered order_Delivered = db.Order_Delivered.Find(id);
            if (order_Delivered == null)
            {
                return NotFound();
            }

            return Ok(order_Delivered);
        }

        // PUT: api/Order_Delivered/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder_Delivered(string id, Order_Delivered order_Delivered)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order_Delivered.Order_ID)
            {
                return BadRequest();
            }

            db.Entry(order_Delivered).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_DeliveredExists(id))
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

        // POST: api/Order_Delivered
        [ResponseType(typeof(Order_Delivered))]
        public IHttpActionResult PostOrder_Delivered(Order_Delivered order_Delivered)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order_Delivered.Add(order_Delivered);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Order_DeliveredExists(order_Delivered.Order_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order_Delivered.Order_ID }, order_Delivered);
        }

        // DELETE: api/Order_Delivered/5
        [ResponseType(typeof(Order_Delivered))]
        public IHttpActionResult DeleteOrder_Delivered(string id)
        {
            Order_Delivered order_Delivered = db.Order_Delivered.Find(id);
            if (order_Delivered == null)
            {
                return NotFound();
            }

            db.Order_Delivered.Remove(order_Delivered);
            db.SaveChanges();

            return Ok(order_Delivered);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Order_DeliveredExists(string id)
        {
            return db.Order_Delivered.Count(e => e.Order_ID == id) > 0;
        }
    }
}