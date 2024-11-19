using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using RelationDataAccess.Implementation;

namespace PerfomanceMeasure.ProductsWithSubcategory
{
    [SimpleJob(RunStrategy.Monitoring, launchCount: 1,
     warmupCount: 2, iterationCount: 5)]
    public class SelectProductWithSubcategoryBenchmark
    {
        SqlProductWithSubCategoryRepository database;

        [GlobalSetup] public void Setup() => database = new("small_db");

        [Benchmark] public async Task GetAllProductsBadWay() => await database.GetAllProductsBadWay();
        [Benchmark] public async Task GetAllProducts() => await database.GetAllProducts();
        [Benchmark] public async Task GetProductById() => await database.GetProductById(4_000_303);
        [Benchmark] public async Task GetProductsBySubCategoryId() => await database.GetProductsBySubCategoryId(11);
        [Benchmark] public async Task GetProductsBySubCategoryName() => await database.GetProductsBySubCategoryName("Toyota");
    }
}
