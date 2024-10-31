using Core.Models;
using Microsoft.AspNetCore.Mvc;
using SQLDataAccess;

namespace API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository= productRepository;
        }

        public async Task<IActionResult> InsertProduct([FromBody] ProductModel product)
        {
            return Ok(await _productRepository.InsertOne(product));
        }
    }
}
