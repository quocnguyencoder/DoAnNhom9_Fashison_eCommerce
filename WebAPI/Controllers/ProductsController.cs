using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private DB_A6A231_DAQLTMDTEntities db = new DB_A6A231_DAQLTMDTEntities();

        // GET: api/Products/5
        //[AcceptVerbs("GET", "POST")]

        [HttpGet]
        [ResponseType(typeof(int))]
        [Route("api/Product/GetStoreid/{userID}")]
        public IHttpActionResult GetStoreid(int userID)
        {
            Store store = (from s in db.Stores where s.UserID == userID select s).FirstOrDefault();
            int id = store.Store_ID;
            return Ok(id);
        }
        [HttpGet]
        [Route("api/Product/GetProducts/{StoreID}")]
        public IQueryable<Product> GetProducts(int StoreID)
        {
            //try
            //{





            var id = new SqlParameter("@StoreID", StoreID);
            var result = db.Database.SqlQuery<Product>("getProducts @StoreID", id).ToList<Product>();
            return result.AsQueryable();


        }
        [HttpGet]
        [Route("api/Product/FindProducts/filter")]
        public IQueryable<Product> FindProducts([FromUri] Find filter)
        {
            //try
            //{


            string Name = filter.Name;
            int TypeID = filter.TypeID;
            int MinAmount = filter.QualityMin;
            int MaxAmount = filter.QualityMax;
            int Store_ID = filter.StoreID;
            var storeid = new SqlParameter("@Store_ID", Store_ID);


            var name = new SqlParameter("@Name", Name ?? "");



            var type = new SqlParameter("@Type", TypeID);
            var min = new SqlParameter("@QualityMin", MinAmount);
            var max = new SqlParameter("@QualityMax", MaxAmount);
            var result = db.Database
            .SqlQuery<Product>("FindProducts @Store_ID,@Name,@Type,@QualityMin,@QualityMax", storeid, name, type, min, max)
            .ToList<Product>();
            return result.AsQueryable();

        }


        // GET: api/Products/5
        [HttpGet]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {

            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            Product product1 = new Product();
            product1.Product_ID = product.Product_ID;
            product1.Name = product.Name;
            product1.Price = product.Price;
            product1.Amount = product.Amount;
            product1.Decription = product.Decription;
            product1.TypeID = product.TypeID;
            product1.Store_ID = product.Store_ID;
            product1.BrandID = product.BrandID;
            product1.Pictures = product.Pictures;
            return Ok(product1);

        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Product_ID)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.Product_ID }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Product_ID == id) > 0;
        }

    }
}