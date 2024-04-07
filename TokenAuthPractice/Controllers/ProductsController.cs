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
using TokenAuthPractice.DAL;
using TokenAuthPractice.Models;
using TokenAuthPractice.CustomFilters;

namespace TokenAuthPractice.Controllers
{
    [CustomExceptionFilter]
    [Authorize(Roles = "Admin")]
    public class ProductsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)

        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found."); // Throw an error that will tigger CustomException Filter
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest); // Throw error using HttpResponseException
            }

            if (id != product.ID)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) //Another way to use HttpResponseException
                {
                    Content = new StringContent(string.Format("Product id is invalid",id))
                };
                throw new HttpResponseException(resp);
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
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound) //Another way to use HttpResponseException
                    {
                        Content = new StringContent(string.Format("Product id does not exist", id))
                    };
                    throw new HttpResponseException(resp);
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
                throw new HttpResponseException(HttpStatusCode.BadRequest); // Throw error using HttpResponseException
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ID }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound) //Another way to use HttpResponseException
                {
                    Content = new StringContent(string.Format("Product is not found", id))
                };
                throw new HttpResponseException(resp);
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
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}