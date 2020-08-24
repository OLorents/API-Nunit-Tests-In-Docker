using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductsController : Controller
    {
        private ProductsDbContext productsDbContext;

        public ProductsController(ProductsDbContext _productsDbContext)

        {
            productsDbContext = _productsDbContext;
        }

        // GET: api/Product
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return productsDbContext.Products;
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var product = productsDbContext.Products.SingleOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound("No Record Found...");
            }

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            productsDbContext.Products.Add(product);
            productsDbContext.SaveChanges(true);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                productsDbContext.Products.Update(product);
                productsDbContext.SaveChanges(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound("No Record Found against this Id...");
            }

            productsDbContext.Products.Update(product);
            productsDbContext.SaveChanges(true);
            return Ok("Product Updated...");
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = productsDbContext.Products.SingleOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound("No Record Found...");
            }

            productsDbContext.Products.Remove(product);
            productsDbContext.SaveChanges(true);
            return Ok("Product deleted");
        }
    }
}