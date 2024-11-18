using Core.Models;
using RelationDataAccess.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Setup
{
    public class SeedService
    {
        public async Task<ChartModel> CreateOneChart(long chartId,string database)
        {
            Random random = new Random();
            ChartModel chartModel = new ChartModel();
            var consumers = await GetConsumers(database);
            var products= await GetProducts(database);
            var consumer = consumers[random.Next(0,consumers.Count-1)];
            chartModel.Consumer = consumer;
            int numOfProducts = random.Next(1, 4);
            chartModel.Products = products.Slice(random.Next(0,products.Count-numOfProducts),numOfProducts);
            return chartModel;
        }

        private async Task<List<ProductModel>> GetProducts(string productDb)
        {
            SqlProductWithSubCategoryRepository sqlProductWithSubCategoryRepository = new(productDb);
            var products= await sqlProductWithSubCategoryRepository.GetAllProducts();
            return products;
        }
        private async Task<List<ConsumerModel>> GetConsumers(string consumerDb)
        {
            SqlConsumerRepository sqlConsumer = new(consumerDb);
            var consumers = await sqlConsumer.GetConsumersOptimised();
            return consumers;
        }
    }
}
