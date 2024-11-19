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
    internal class UpdateProductWithSubcategoryBenchmark
    {
        public SqlProductWithSubCategoryRepository database;

        [GlobalSetup] public void Setup() => database = new("small_db");
    }
}
