using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ExternalData;
using Core.Models;

namespace Services
{
    public static class ChartGeneratorService
    {
        public static List<ChartModel> GenerateChartsList(IEnumerable<ConsumerModel> consumers,IEnumerable<ProductModel> products)
        {
            List<ChartModel> charts = new();
            Random random = new Random();
            int length= consumers.Count();
            
            for(int i = 0; i < length; i++)
            {
                if (i % 4 == 0)
                {
                    continue;
                }
                ChartModel chart = new ChartModel();
                chart.Consumer = consumers.ElementAt(i);
                chart.CreatedAt = DateTime.Now;
                chart.UpdatedAt = DateTime.Now;
                chart.Status = ChartStatus.Active;
                chart.Products= new List<ProductModel>();

                HashSet<long> productsId = new HashSet<long>();
                int numOfProducts = random.Next(1, 5);
                for(int j=0;j < numOfProducts; j++)
                {
                    while (true)
                    {
                        int index = random.Next(products.Count());
                        ProductModel product = products.ElementAt(index);

                        if (productsId.Add(product.Id))
                        {
                            chart.Products.Add(product);
                            break;
                        }

                    }

                }
                charts.Add(chart);
            }
        }
    }
}
