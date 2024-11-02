using BenchmarkDotNet.Attributes;
using SQLDataAccess.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfomanceMeasure
{
    [SimpleJob(launchCount: 1, warmupCount: 2, iterationCount: 6)]
    public class ProductReadBench
    {
        private ProductRepository productRepository;
        [GlobalSetup]
        public void Setup() 
        {
            productRepository = new ProductRepository();
        }

        [Benchmark]
        public async Task JoinWithStar()
        {
            var products = await productRepository.GetAllProductsWithCompaniesBySelect();

        }
            [Benchmark]
        public async Task Join() 
        {
            var products = await productRepository.GetAllProductsWithCompaniesBySelectOptimised();
        }

        [Benchmark]
        public async Task SubQuery()
        {
            var products = await productRepository.GetAllProductsWithCompaniesBySelectSubQuery();
        }

        [Benchmark]
        public async Task SubQueryApply()
        {
            var products = await productRepository.GetAllProductsWithCompaniesBySubQueryApply();
        }
    }
}
