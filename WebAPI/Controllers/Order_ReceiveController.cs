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
    public class Order_ReceiveController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Order_Receive
        public IQueryable<Order_Receive> GetOrder_Receive()
        {
            return db.Order_Receive;
        }

        // GET: api/Order_Receive/5
        [ResponseType(typeof(Order_Receive))]
        public IHttpActionResult GetOrder_Receive(string id)
        {
            Order_Receive order_Receive = db.Order_Receive.Find(id);
            if (order_Receive == null)
            {
                return NotFound();
            }

            return Ok(order_Receive);
        }

        // PUT: api/Order_Receive/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder_Receive(string id, Order_Receive order_Receive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order_Receive.Order_ID)
            {
                return BadRequest();
            }

            db.Entry(order_Receive).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_ReceiveExists(id))
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

        // POST: api/Order_Receive
        [ResponseType(typeof(Order_Receive))]
        public IHttpActionResult PostOrder_Receive(Order_Receive order_Receive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order_Receive.Add(order_Receive);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Order_ReceiveExists(order_Receive.Order_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order_Receive.Order_ID }, order_Receive);
        }

        // DELETE: api/Order_Receive/5
        [ResponseType(typeof(Order_Receive))]
        public IHttpActionResult DeleteOrder_Receive(string id)
        {
            Order_Receive order_Receive = db.Order_Receive.Find(id);
            if (order_Receive == null)
            {
                return NotFound();
            }

            db.Order_Receive.Remove(order_Receive);
            db.SaveChanges();

            return Ok(order_Receive);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Order_ReceiveExists(string id)
        {
            return db.Order_Receive.Count(e => e.Order_ID == id) > 0;
        }
    }
}