using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using PerfomanceMeasure.Consumer.Insert;
using PerfomanceMeasure.Consumer.Select;
using PerfomanceMeasure.Consumer.Update;
using PerfomanceMeasure.ProductsWithSubcategory;

namespace PerfomanceMeasure
{
    internal class Program
    {
        static void Main(string[] args)
        {
           // var config = ManualConfig.Create(DefaultConfig.Instance)
             //   .WithArtifactsPath(@"C:\Benchmarks");
            BenchmarkRunner.Run<InsertProductWithSubcategoryHybridBenchmark>();
        }
    }
}
