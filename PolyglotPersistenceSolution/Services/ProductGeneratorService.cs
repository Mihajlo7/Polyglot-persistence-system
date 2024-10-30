using Core.Data.Mongo;
using Core.ExternalData;
using Core.Models;
using System.Security.Cryptography.X509Certificates;

namespace Services
{
    public static class ProductGeneratorService
    {
        public static List<ProductModel> GenerateProductsList(List<SellerModel> sellers, IEnumerable<ProductAndDetailsEx> productsRaw)
        {
            List<ProductModel> products = new();
            Random rand = new Random();
            int x = 0;
            int productNum = 1;
            int length=sellers.Count;
            foreach (var productRaw in productsRaw)
            {
                ProductModel product = new ProductModel()
                {
                    Name = productRaw.Name,
                    Id=productNum,
                    Price = productRaw.Price,
                    Distribute= new List<SellerModel>(),
                    SubCategory= new SubCategoryModel() { Name=productRaw.SubCategory}
                };

                // get random number
                int index= rand.Next(length);
                SellerModel seller= sellers[index];

                product.Produced = seller;
                if (seller.HasShop)
                {
                    product.Store = seller;
                }
                else
                {
                    x++;
                }

                if (x == 7)
                {
                    while (true)
                    {
                        index = rand.Next(length);
                        seller = sellers[index];
                        if (seller.Id != product.Produced.Id)
                            break;
                    }
                    product.Store = seller;
                    x = 0;
                }

                int numberOfDistributors = rand.Next(1, 5);
                long[] guids = new long[numberOfDistributors];
                for (int i = 0; i < numberOfDistributors; i++) 
                {
                    index = rand.Next(length);
                    seller = sellers[index];

                    while (true)
                    {
                        index = rand.Next(length);
                        seller = sellers[index];
                        if (seller.Id != product.Produced.Id && !guids.Contains(seller.Id))
                            break;
                    }
                    product.Distribute.Add(seller);
                    guids[i] = seller.Id;
                }

                if(productRaw is CarEx carRaw)
                {
                    product.Details = new CarDetailsModel()
                    {
                        ShortDescription = carRaw.ShortDescription,
                        ImageUrl = carRaw.ImageUrl,
                        Model = carRaw.SubCategory,
                        SerialNumber = carRaw.SerialNumber,
                        EngineDisplacement = $"{carRaw.EngineDisplacement} cm3",
                        EnginePower = $"{carRaw.EnginePower} hp",
                        YearManifactured = carRaw.Year,
                        LongDescription = carRaw.LongDescription,
                    };
                }
                products.Add(product);
            }
            return products;
        }
    }
}
