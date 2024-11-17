using Core.Models;
using Microsoft.AspNetCore.Mvc;
using RelationDataAccess.Implementation;

namespace API.Controllers
{
    [ApiController]
    [Route("api/subcategory/products")]
    public class ProductWithSubcategoryController
    {
        [HttpPost("insertManyBulk")]
        public async Task InsertManyBulk([FromBody] List<ProductModel> products)
        {
            SqlProductWithSubCategoryRepository db = new("small_db");
            await db.InsertManyBulk(products);
        }
    }
}
