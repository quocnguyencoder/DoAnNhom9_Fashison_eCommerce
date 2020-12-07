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
    public class Order_DeliveringController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Order_Delivering
        public IQueryable<Order_Delivering> GetOrder_Delivering()
        {
            return db.Order_Delivering;
        }

        // GET: api/Order_Delivering/5
        [ResponseType(typeof(Order_Delivering))]
        public IHttpActionResult GetOrder_Delivering(string id)
        {
            Order_Delivering order_Delivering = db.Order_Delivering.Find(id);
            if (order_Delivering == null)
            {
                return NotFound();
            }

            return Ok(order_Delivering);
        }

        // PUT: api/Order_Delivering/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder_Delivering(string id, Order_Delivering order_Delivering)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order_Delivering.Order_ID)
            {
                return BadRequest();
            }

            db.Entry(order_Delivering).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_DeliveringExists(id))
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

        // POST: api/Order_Delivering
        [ResponseType(typeof(Order_Delivering))]
        public IHttpActionResult PostOrder_Delivering(Order_Delivering order_Delivering)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order_Delivering.Add(order_Delivering);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Order_DeliveringExists(order_Delivering.Order_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order_Delivering.Order_ID }, order_Delivering);
        }

        // DELETE: api/Order_Delivering/5
        [ResponseType(typeof(Order_Delivering))]
        public IHttpActionResult DeleteOrder_Delivering(string id)
        {
            Order_Delivering order_Delivering = db.Order_Delivering.Find(id);
            if (order_Delivering == null)
            {
                return NotFound();
            }

            db.Order_Delivering.Remove(order_Delivering);
            db.SaveChanges();

            return Ok(order_Delivering);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Order_DeliveringExists(string id)
        {
            return db.Order_Delivering.Count(e => e.Order_ID == id) > 0;
        }
    }
}