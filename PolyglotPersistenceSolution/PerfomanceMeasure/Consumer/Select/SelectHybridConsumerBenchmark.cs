using BenchmarkDotNet.Attributes;
using HybridDataAccess.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfomanceMeasure.Consumer.Select
{
    [SimpleJob(launchCount: 1,
     warmupCount: 2, iterationCount: 5)]
    public class SelectHybridConsumerBenchmark
    {
        HybridConsumerRepository small_db = new("hybrid_small_db");

        [Benchmark]
        public async Task GetAllConsumersBadWaySmall()
        {
            await small_db.GetConsumers();
        }

        [Benchmark]
        public async Task GetAllConsumersOptimisedSmall()
        {
            await small_db.GetConsumersOptimised();
        }

        [Benchmark]
        public async Task GetAllConsumerByIdSmall()
        {
            await small_db.GetConsumerById(1_000_100);
        }

        [Benchmark]
        public async Task GetAllConsumerByEmailSmall()
        {
            await small_db.GetConsumerByEmail("creinert2n@time.com");
        }

        [Benchmark]
        public async Task GetAllConsumerByFriendEmailSmall()
        {
            await small_db.GetConsumersByFriendEmail("creinert2n@time.com");
        }

        [Benchmark]
        public async Task GetConsumersByEmailAndFriendshipLevelSmall()
        {
            await small_db.GetConsumersByEmailAndFriendshipLevel("creinert2n@time.com", 5);
        }
    }
}
