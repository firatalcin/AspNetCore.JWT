using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTProjectEx1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public IActionResult GetProducts()
        {
            List<Product> products = new List<Product>() 
            {
                new Product{Name="Elma"},
                new Product{Name="Armut"},
                new Product{Name="Portakal"}
            };

            return Ok(products);
        }
    }
}
