using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Web.Models;
namespace E_Commerce.Web.Controllers
{
    [Route("api/[controller]")] // BaseUrl/api/product
    [ApiController] // Automatic Model Validation
    public class ProductController : ControllerBase
    {
        [HttpGet("Id")]
        public ActionResult<Product> Get(int id)
        {
            return new Product() { Id = id };
        }
        
        [HttpGet]
        public ActionResult<Product> GetAll()
        {
            return new Product() { Id = 100 };
        }

        [HttpPost]
        public ActionResult<Product> AddProduct(Product roduct)
        {
            return new Product();
        }

        [HttpPut]
        public ActionResult<Product> UpdateProduct(Product roduct)
        {
            return new Product();
        }
        
        
        [HttpDelete]
        public ActionResult<Product> DeleteProduct(Product roduct)
        {
            return new Product();
        }
    }
}
