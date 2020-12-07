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
    public class Order_ConfirmController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Order_Confirm
        public IQueryable<Order_Confirm> GetOrder_Confirm()
        {
            return db.Order_Confirm;
        }

        // GET: api/Order_Confirm/5
        [ResponseType(typeof(Order_Confirm))]
        public IHttpActionResult GetOrder_Confirm(string id)
        {
            Order_Confirm order_Confirm = db.Order_Confirm.Find(id);
            if (order_Confirm == null)
            {
                return NotFound();
            }

            return Ok(order_Confirm);
        }

        // PUT: api/Order_Confirm/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder_Confirm(string id, Order_Confirm order_Confirm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order_Confirm.Order_ID)
            {
                return BadRequest();
            }

            db.Entry(order_Confirm).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_ConfirmExists(id))
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

        // POST: api/Order_Confirm
        [ResponseType(typeof(Order_Confirm))]
        public IHttpActionResult PostOrder_Confirm(Order_Confirm order_Confirm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order_Confirm.Add(order_Confirm);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Order_ConfirmExists(order_Confirm.Order_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order_Confirm.Order_ID }, order_Confirm);
        }

        // DELETE: api/Order_Confirm/5
        [ResponseType(typeof(Order_Confirm))]
        public IHttpActionResult DeleteOrder_Confirm(string id)
        {
            Order_Confirm order_Confirm = db.Order_Confirm.Find(id);
            if (order_Confirm == null)
            {
                return NotFound();
            }

            db.Order_Confirm.Remove(order_Confirm);
            db.SaveChanges();

            return Ok(order_Confirm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Order_ConfirmExists(string id)
        {
            return db.Order_Confirm.Count(e => e.Order_ID == id) > 0;
        }
    }
}