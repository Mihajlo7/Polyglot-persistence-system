using HybridDataAccess.Implementation;
using Microsoft.AspNetCore.Mvc;
using RelationDataAccess.Implementation;
using Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        [HttpGet("products")]
        public async Task<IActionResult> SetProductsToSubCategories()
        {
            SqlProductWithSubCategoryRepository sqlProductWithSubCategoryRepository = new("small_db");
            var products=await sqlProductWithSubCategoryRepository.GetAllProducts();
            var subCategories= products.ToSubCategoriesFromProducts();
            HybridProductWithSubCategoryRepository hybridProductWithSubCategoryRepository = new("hybrid_small_db");

            await hybridProductWithSubCategoryRepository.InsertManyBulkOpt(subCategories);
            return Ok(subCategories);
        }
        [HttpGet("consumers")]
        public async Task<IActionResult> SetConsumersAndFriends()
        {
            SqlConsumerRepository sqlProductWithSubCategoryRepository = new("large_db");
            var consumers = await sqlProductWithSubCategoryRepository.GetConsumersOptimised();
            HybridConsumerRepository hybridConsumerRepository = new("hybrid_small_db");
            await hybridConsumerRepository.DeleteConsumersFriends();
            await hybridConsumerRepository.InsertManyFriendBulk(consumers);
            return Ok("Setup hybrid database");
        }
    }
}
