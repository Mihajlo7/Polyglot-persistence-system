using BenchmarkDotNet.Running;
using PerfomanceMeasure.Consumer.Insert;

namespace PerfomanceMeasure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<InsertConsumerBenchMark>();
        }
    }
}
