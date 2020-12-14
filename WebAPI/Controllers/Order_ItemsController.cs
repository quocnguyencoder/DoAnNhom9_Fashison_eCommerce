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
    public class Order_ItemsController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Order_Items
        public IQueryable<Order_Items> GetOrder_Items()
        {
            return db.Order_Items;
        }

        // GET: api/Order_Items/5
        public IHttpActionResult GetOrder_Items(string id)
        {
            return Ok(db.sp_getOrderDetail(id));
        }

        // PUT: api/Order_Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder_Items(string id, Order_Items order_Items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order_Items.Order_ID)
            {
                return BadRequest();
            }

            db.Entry(order_Items).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_ItemsExists(id))
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

        // POST: api/Order_Items
        [ResponseType(typeof(Order_Items))]
        public IHttpActionResult PostOrder_Items(Order_Items order_Items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order_Items.Add(order_Items);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Order_ItemsExists(order_Items.Order_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order_Items.Order_ID }, order_Items);
        }

        // DELETE: api/Order_Items/5
        [ResponseType(typeof(Order_Items))]
        public IHttpActionResult DeleteOrder_Items(string id)
        {
            Order_Items order_Items = db.Order_Items.Find(id);
            if (order_Items == null)
            {
                return NotFound();
            }

            db.Order_Items.Remove(order_Items);
            db.SaveChanges();

            return Ok(order_Items);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Order_ItemsExists(string id)
        {
            return db.Order_Items.Count(e => e.Order_ID == id) > 0;
        }
    }
}