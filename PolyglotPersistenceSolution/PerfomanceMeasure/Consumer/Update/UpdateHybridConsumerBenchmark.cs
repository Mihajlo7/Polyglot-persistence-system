using BenchmarkDotNet.Attributes;
using HybridDataAccess.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfomanceMeasure.Consumer.Update
{
    [SimpleJob(launchCount: 1,
     warmupCount: 2, iterationCount: 5)]
    public class UpdateHybridConsumerBenchmark
    {
        HybridConsumerRepository database;
        List<string> telephones = new List<string>()
        {
            "000-000-0000",
            "000-000-0001",
            "000-000-0002",
            "000-000-0003",
            "000-000-0004"
        };
        List<int> friendshipLevels = new List<int>() { 5, 6, 7, 8, 6 };

        [GlobalSetup]
        public void Setup()
        {
            database = new("hybrid_small_db");
            
        }

        [Benchmark]
        public async Task UpdateConsumerTelephoneById()
        {
            await database.UpdateConsumerTelephoneById(1000014, telephones[0]);
        }
        [Benchmark]
        public async Task UpdateConsumerTelephoneByEmail()
        {
            await database.UpdateConsumerTelephoneByEmail("eberthote@ft.com", telephones[0]);
        }
        [Benchmark]
        public async Task UpdateConsumerFriendByName()
        {
            await database.UpdateConsumersByName("Stefan", friendshipLevels[0]);
        }
        [Benchmark]
        public async Task UpdateConsumerFriendshipById()
        {
            await database.UpdateConsumersFriendshipById(1000014, friendshipLevels[0]);
        }
    }
}
