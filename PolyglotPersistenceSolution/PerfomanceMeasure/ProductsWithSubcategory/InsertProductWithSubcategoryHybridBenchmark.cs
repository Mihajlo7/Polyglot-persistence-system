using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Core.Models;
using HybridDataAccess.Implementation;
using RelationDataAccess.Implementation;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfomanceMeasure.ProductsWithSubcategory
{
    [SimpleJob(RunStrategy.Monitoring, launchCount: 1,
     warmupCount: 2, iterationCount: 5)]
    public class InsertProductWithSubcategoryHybridBenchmark
    {
        public HybridProductWithSubCategoryRepository _database = new("hybrid_small_db");
        public SqlProductWithSubCategoryRepository _db = new("small_db");
        public List<ProductModel> dataSet { get; set; }
        public List<SubCategoryModel> dataSetSubcategory { get; set; } = new();

        
        [GlobalSetup]
        public async Task GlobalSetup()
        {
            dataSet = await _db.GetAllProducts();
            dataSetSubcategory=dataSet.ToSubCategoriesFromProducts();
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
            await _database.InsertManyBulkOpt(dataSetSubcategory);
        }
        [Benchmark]
        public async Task InsertOne()
        {
            await _database.InsertOne(dataSet.First());
        }
        
    }
}
