using ProductsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsService.Controllers
{
    public class ProductsController : ApiController
    {
        [ActionName("getallproducts")]
        [HttpGet]
        public IEnumerable<Product> SelectAllProducts()
        {
            using (BDEntities entities = new BDEntities())
            {
                return entities.Products.ToList();
            }
        }
        [ActionName("getproductbyid")]
        [HttpGet]
        public Product GetProductById(int id)
        {
            using (BDEntities entities = new BDEntities())
            {
                return entities.Products.FirstOrDefault(e => e.ID == id);
            }
        }
        [ActionName("getproductbyname")]
        [HttpGet]
        public Product GetProductByName(string name)
        {
            using (BDEntities entities = new BDEntities())
            {
                return entities.Products.FirstOrDefault(e => e.ProductName.Contains(name));
            }
        }
        [ActionName("getavailableproducts")]
        [HttpGet]
        public IEnumerable<Product> GetAvailableProducts()
        {
            using (BDEntities entities = new BDEntities())
            {
                return entities.Products.Where(e => e.stock > 0).ToList();
            
            }
        }
        [ActionName("addnewproduct")]
        [HttpGet]
        public IHttpActionResult AddNewProduct(Product pr)
        {
            using (BDEntities entities = new BDEntities())
            {
                if (!string.IsNullOrEmpty(pr.ProductName))
                {
                    Product p = new Product();
                    p.stock = pr.stock;
                    p.ProductName = pr.ProductName;
                    entities.Products.Add(p);
                    entities.SaveChanges();

                    return Ok(p);
                }
                else
                {
                    return BadRequest();
                }

            }
        }

        public IHttpActionResult UpdateProduct(Product pr)
        {
            using (BDEntities entities = new BDEntities())
            {
                if (!string.IsNullOrEmpty(pr.ProductName) && pr.ID > 0)
                {
                    Product product = entities.Products.FirstOrDefault(p => p.ID == pr.ID);
                    product.stock = pr.stock;
                    product.ProductName = pr.ProductName;
                    entities.SaveChanges();

                    return Ok(product);
                }
                else
                {
                    return NotFound();
                }
            }
        }

    }
}
