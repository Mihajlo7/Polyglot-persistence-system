using Core.Models;
using HybridDataAccess.Implementation;
using Microsoft.AspNetCore.Mvc;
using RelationDataAccess.Implementation;
using Services;
using Services.JsonWorker;
using Services.Setup;

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

        [HttpGet("products/large")]
        public async Task InsertProductsLarge()
        {
            SqlProductWithSubCategoryRepository productWithSubCategoryRepository = new("small_db");

            JsonWorkerClass jsonWorker = new("Data");

            productWithSubCategoryRepository = new("large_db");
            var products = jsonWorker.ReadObjectsFromFile<ProductModel>("Products_Large.json");
            int res = await productWithSubCategoryRepository.InsertMany(products);
        }
        [HttpGet("charts")]
        public async Task<ChartModel> InsertCharts()
        {
            SqlOrderRepository sqlOrderRepository = new("small_db");
            /*
            SeedService seedService = new();
            var chart = await seedService.CreateOneChart(0,"small_db");
            await sqlOrderRepository.InsertChart(chart);
            return chart;
            */
            await sqlOrderRepository.DeleteCharts();
            return null;
        }
    }
}
