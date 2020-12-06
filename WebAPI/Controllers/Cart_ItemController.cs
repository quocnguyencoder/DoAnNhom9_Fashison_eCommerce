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
    public class Cart_ItemController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Cart_Item
        public IQueryable<Cart_Item> GetCart_Item()
        {
            return db.Cart_Item;
        }

        // GET: api/Cart_Item/5
        [ResponseType(typeof(Cart_Item))]
        public IHttpActionResult GetCart_Item(int ItemID,int CartID)
        {
            Cart_Item cart_Item = db.Cart_Item.Find(ItemID,CartID);
            if (cart_Item == null)
            {
                return NotFound();
            }

            return Ok(cart_Item);
        }

        // PUT: api/Cart_Item/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCart_Item(int ItemID, int CartID, Cart_Item cart_Item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ItemID != cart_Item.ItemID && CartID != cart_Item.Cart_ID)
            {
                return BadRequest();
            }

            db.Entry(cart_Item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!Cart_ItemExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cart_Item
        [ResponseType(typeof(Cart_Item))]
        public IHttpActionResult PostCart_Item(Cart_Item cart_Item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cart_Item.Add(cart_Item);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Cart_ItemExists(cart_Item.ItemID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cart_Item.ItemID }, cart_Item);
        }

        // DELETE: api/Cart_Item/5
        [ResponseType(typeof(Cart_Item))]
        public IHttpActionResult DeleteCart_Item(int ItemID, int CartID)
        {
            Cart_Item cart_Item = db.Cart_Item.Find(ItemID,CartID);
            if (cart_Item == null)
            {
                return NotFound();
            }

            db.Cart_Item.Remove(cart_Item);
            db.SaveChanges();

            return Ok(cart_Item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Cart_ItemExists(int id)
        {
            return db.Cart_Item.Count(e => e.ItemID == id) > 0;
        }
    }
}