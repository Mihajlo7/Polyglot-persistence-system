using BenchmarkDotNet.Attributes;
using RelationDataAccess.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace PerfomanceMeasure.Consumer.Update
{
    [SimpleJob(launchCount: 1,
     warmupCount: 2, iterationCount: 5)]
    public class UpdateConsumerBenchmark
    {
        SqlConsumerRepository database;
        int index;
        List<string> telephones= new List<string>()
        {
            "000-000-0000",
            "000-000-0001",
            "000-000-0002",
            "000-000-0003",
            "000-000-0004"
        };
        List<int> friendshipLevels = new List<int>() {5,6,7,8,6};

        [GlobalSetup]
        public void Setup() 
        {
            database = new("medium_db");
            index = 0;
        }

        [Benchmark]
        public async Task UpdateConsumerTelephoneById()
        {
            await database.UpdateConsumerTelephoneById(1000012, telephones[0]);
        }
        [Benchmark]
        public async Task UpdateConsumerTelephoneByEmail()
        {
            await database.UpdateConsumerTelephoneByEmail("kripperc@mtv.com", telephones[0]);
        }
        [Benchmark]
        public async Task UpdateConsumerFriendByName()
        {
            await database.UpdateConsumersByName("Stefan", friendshipLevels[0]);
        }
        [Benchmark]
        public async Task UpdateConsumerFriendshipById()
        {
            await database.UpdateConsumersFriendshipById(1000012, friendshipLevels[0]);
        }
    }
}
