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
        [HttpPost("insert-one")]
        public async Task<IActionResult> InsertProduct([FromBody] ProductModel product)
        {
            return Ok(await _productRepository.InsertOne(product));
        }

        [HttpPost("insert-many")]
        public async Task<IActionResult> InsertProduct([FromBody] List<ProductModel> products)
        {
            return Ok(await _productRepository.InsertMany(products));
        }
        [HttpGet("select-join")]
        public async Task<IActionResult> GetProductsBySelect()
        {
            return Ok(await _productRepository.GetAllProductsWithCompaniesBySelect());
        }
        [HttpGet("select-join-opt")]
        public async Task<IActionResult> GetProductsBySelectOpt()
        {
            return Ok(await _productRepository.GetAllProductsWithCompaniesBySelectOptimised());
        }

        [HttpGet("select-subQuery")]
        public async Task<IActionResult> GetProductsBySelectSubQuery()
        {
            return Ok(await _productRepository.GetAllProductsWithCompaniesBySelectSubQuery());
        }
    }
}
