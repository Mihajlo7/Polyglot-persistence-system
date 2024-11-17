using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Core.Models;
using RelationDataAccess.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfomanceMeasure.ProductsWithSubcategory
{
    [SimpleJob(RunStrategy.Monitoring, launchCount: 1,
     warmupCount: 2, iterationCount: 5)]
    public class InsertProductWithSubcategoryBenchmark
    {
        public SqlProductWithSubCategoryRepository _database=new("large_db");
        public List<ProductModel> dataSet { get; set; }

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            dataSet= await _database.GetAllProducts();
            await _database.DeleteAllProducts();
        }
        [IterationSetup]
        public void IterationSetup()
        {
            _database.DeleteAllProducts().GetAwaiter().GetResult();
        }

        [Benchmark]
        public async Task InsertManyProducts()
        {
            await _database.InsertMany(dataSet);
        }
        [Benchmark]
        public async Task InsertBulk()
        {
            await _database.InsertManyBulk(dataSet);
        }
        [Benchmark]
        public async Task InsertOne()
        {
            await _database.InsertOne(dataSet.First());
        }
        [GlobalCleanup]
        public async Task GlobalCleanUp() 
        {
            var products= await _database.GetAllProducts();
            if (products.Count < 2)
            {
                await _database.DeleteAllProducts();
                await _database.InsertManyBulk(dataSet);
            }
        }
    }
}
