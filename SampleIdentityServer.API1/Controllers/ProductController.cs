using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleIdentityServer.API1.Model;

namespace SampleIdentityServer.API1.Controllers
{
    [Route("api/[controller]/[action]")] //action bazlı bir erişim sağlancak 
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        [HttpGet, Authorize(Policy ="ReadProduct")] //yetkilendirmeyi bir alt seviye indirirerek token içerisinden gelen scope da ki bilgileride içermesi gereiyor eğerlki içermez ise erişim sağlanamayacaktır. bu Autheorize attributu bu görevi görecek.
        public IActionResult GetProduct()
        {
            var productlist = new List<Product>() 
            {
                new Product { Id = 1, Name = "Silgi", Price = 100, Stock = 500 },
                new Product { Id = 1, Name = "Kalem", Price = 100, Stock = 500 },
                new Product { Id = 1, Name = "Bant", Price = 100, Stock = 500 },
                new Product { Id = 1, Name = "Defter", Price = 100, Stock = 500 },
                new Product { Id = 1, Name = "Kitap", Price = 100, Stock = 500 }
            };

            return Ok(productlist);
        }
        [Authorize(Policy ="UpdateOrCreate")]
        public IActionResult UpdateProduct(int id)
        {
            return Ok($"id'si{id} olan product güncellenmiştir.");
        }
        [Authorize(Policy = "UpdateOrCreate")]
        public IActionResult CreateProduct(Product product) 
        {
            return Ok(product);
        }
    }
}
