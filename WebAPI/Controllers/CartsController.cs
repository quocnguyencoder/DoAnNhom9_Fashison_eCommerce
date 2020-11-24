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
    public class CartsController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Carts
        public IQueryable<User_Cart_Result> GetCarts(int id)
        {
            return db.User_Cart(id);
        }

        //// GET: api/Carts/5
        //[ResponseType(typeof(Cart))]
        //public IHttpActionResult GetCart(int id)
        //{
        //    Cart cart = db.Carts.Find(id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(cart);
        //}

        //// PUT: api/Carts/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutCart(int id, Cart cart)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != cart.Cart_ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(cart).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CartExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Carts
        //[ResponseType(typeof(Cart))]
        //public IHttpActionResult PostCart(Cart cart)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Carts.Add(cart);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = cart.Cart_ID }, cart);
        //}

        //// DELETE: api/Carts/5
        //[ResponseType(typeof(Cart))]
        //public IHttpActionResult DeleteCart(int id)
        //{
        //    Cart cart = db.Carts.Find(id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Carts.Remove(cart);
        //    db.SaveChanges();

        //    return Ok(cart);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool CartExists(int id)
        //{
        //    return db.Carts.Count(e => e.Cart_ID == id) > 0;
        //}
    }
}