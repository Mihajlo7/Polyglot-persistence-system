using BenchmarkDotNet.Running;

namespace PerfomanceMeasure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<TestBench>();
        }
    }
}
