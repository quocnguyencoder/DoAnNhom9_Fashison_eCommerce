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
    public class Buyer_OrdersController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Buyer_Orders
        public IQueryable<view_Buyer_Orders> Getview_Buyer_Orders()
        {
            return db.view_Buyer_Orders;
        }

        // GET: api/Buyer_Orders/5
        public IHttpActionResult Getview_Buyer_Orders(int id)
        {
            return Ok(db.getOrders_Of_User(id));
        }

        // PUT: api/Buyer_Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putview_Buyer_Orders(string id, view_Buyer_Orders view_Buyer_Orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != view_Buyer_Orders.Order_ID)
            {
                return BadRequest();
            }

            db.Entry(view_Buyer_Orders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!view_Buyer_OrdersExists(id))
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

        // POST: api/Buyer_Orders
        [ResponseType(typeof(view_Buyer_Orders))]
        public IHttpActionResult Postview_Buyer_Orders(view_Buyer_Orders view_Buyer_Orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.view_Buyer_Orders.Add(view_Buyer_Orders);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (view_Buyer_OrdersExists(view_Buyer_Orders.Order_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = view_Buyer_Orders.Order_ID }, view_Buyer_Orders);
        }

        // DELETE: api/Buyer_Orders/5
        [ResponseType(typeof(view_Buyer_Orders))]
        public IHttpActionResult Deleteview_Buyer_Orders(string id)
        {
            view_Buyer_Orders view_Buyer_Orders = db.view_Buyer_Orders.Find(id);
            if (view_Buyer_Orders == null)
            {
                return NotFound();
            }

            db.view_Buyer_Orders.Remove(view_Buyer_Orders);
            db.SaveChanges();

            return Ok(view_Buyer_Orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool view_Buyer_OrdersExists(string id)
        {
            return db.view_Buyer_Orders.Count(e => e.Order_ID == id) > 0;
        }
    }
}