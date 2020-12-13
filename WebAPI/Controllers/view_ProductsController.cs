using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI;

namespace WebAPI.Controllers
{
    public class view_ProductsController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/view_Products
        public IQueryable<view_Products> Getview_Products()
        {
            return db.view_Products.OrderByDescending(x=> x.Name);
        }

        // GET: api/view_Products/5
        [ResponseType(typeof(view_Products))]
        public IHttpActionResult Getview_Products(int id)
        {
            view_Products view_Products = db.view_Products.Where(x => x.Product_ID == id).FirstOrDefault();
            if (view_Products == null)
            {
                return NotFound();
            }

            return Ok(view_Products);
        }

        // PUT: api/view_Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putview_Products(int id, view_Products view_Products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != view_Products.Product_ID)
            {
                return BadRequest();
            }

            db.Entry(view_Products).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!view_ProductsExists(id))
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

        // POST: api/view_Products
        [ResponseType(typeof(view_Products))]
        public IHttpActionResult Postview_Products(view_Products view_Products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.view_Products.Add(view_Products);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = view_Products.Product_ID }, view_Products);
        }

        // DELETE: api/view_Products/5
        [ResponseType(typeof(view_Products))]
        public IHttpActionResult Deleteview_Products(int id)
        {
            view_Products view_Products = db.view_Products.Where(x => x.Product_ID == id).FirstOrDefault();
            if (view_Products == null)
            {
                return NotFound();
            }

            db.view_Products.Remove(view_Products);
            db.SaveChanges();

            return Ok(view_Products);
        }

        // Get products by Type_ID
        [HttpGet]
        [Route("api/view_Products/Type/{typeID}")]
        public List<view_Products> GetView_ProductsByType(int typeID)
        {
            return db.view_Products.Where(x => x.TypeID == typeID).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool view_ProductsExists(int id)
        {
            return db.view_Products.Count(e => e.Product_ID == id) > 0;
        }


    }
}