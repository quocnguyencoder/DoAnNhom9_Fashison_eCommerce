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
    public class Buyer_LoadProductController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Buyer_LoadProduct
        public IQueryable<Buyer_LoadProduct> GetBuyer_LoadProduct()
        {
            return db.Buyer_LoadProduct;
        }

        // GET: api/Buyer_LoadProduct/5
        [ResponseType(typeof(get_ProductByID_Result))]
        public IHttpActionResult GetBuyer_LoadProduct(int id)
        {
            return Ok(db.get_ProductByID(id));
        }

        // PUT: api/Buyer_LoadProduct/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuyer_LoadProduct(int id, Buyer_LoadProduct buyer_LoadProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buyer_LoadProduct.Product_ID)
            {
                return BadRequest();
            }

            db.Entry(buyer_LoadProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Buyer_LoadProductExists(id))
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

        // POST: api/Buyer_LoadProduct
        [ResponseType(typeof(Buyer_LoadProduct))]
        public IHttpActionResult PostBuyer_LoadProduct(Buyer_LoadProduct buyer_LoadProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Buyer_LoadProduct.Add(buyer_LoadProduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = buyer_LoadProduct.Product_ID }, buyer_LoadProduct);
        }

        // DELETE: api/Buyer_LoadProduct/5
        [ResponseType(typeof(Buyer_LoadProduct))]
        public IHttpActionResult DeleteBuyer_LoadProduct(int id)
        {
            Buyer_LoadProduct buyer_LoadProduct = db.Buyer_LoadProduct.Find(id);
            if (buyer_LoadProduct == null)
            {
                return NotFound();
            }

            db.Buyer_LoadProduct.Remove(buyer_LoadProduct);
            db.SaveChanges();

            return Ok(buyer_LoadProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Buyer_LoadProductExists(int id)
        {
            return db.Buyer_LoadProduct.Count(e => e.Product_ID == id) > 0;
        }

        [HttpGet]
        [Route("api/Buyer_LoadProduct/Type/{typeid}")]
        public IHttpActionResult GetProduct_byType(int typeid)
        {
            return Ok(db.sp_ProductByType(typeid));
        }


        [Route("api/Buyer_LoadProduct/Store/{storeid}")]
        public IHttpActionResult GetProduct_byStore(int storeid)
        {
            return Ok(db.sp_ProductByStore(storeid));
        }

        [HttpGet]
        [Route("api/Buyer_LoadProduct/Search/{typeid}/{name}")]
        public IHttpActionResult Search(string name, int typeid)
        {
            return Ok(db.sp_searchProduct(name,typeid));
        }
    }
}