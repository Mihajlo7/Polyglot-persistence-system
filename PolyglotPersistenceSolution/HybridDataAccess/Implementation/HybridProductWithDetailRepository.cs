using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using IDataAccess;

namespace HybridDataAccess.Implementation
{
    public class HybridProductWithDetailRepository : IProductWithDetailsRepository
    {
        private readonly string _database;
        private readonly string _connectionString;

        public HybridProductWithDetailRepository(string database)
        {
            _database = database;
            _connectionString = $"Data Source=.;Initial Catalog={_database};Integrated Security=True;TrustServerCertificate=True;";
        }

        public Task<List<ProductModel>> GetProductsWithDetailByProductName(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductModel>> GetProductsWithDetailsByJoin()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductModel>> GetProductsWithDetailsByJoinOptimised()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductModel>> GetProductsWithDetailsBySubQuery()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductModel>> GetProductsWithDetailsBySubQueryUsingApply()
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> GetProductWithDetailByProductId(long productId)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertMany(List<ProductModel> product)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertManyBulk(List<ProductModel> products)
        {
            throw new NotImplementedException();
        }

        public Task InsertOne(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLongDescriptionByProductId(long productId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePriceByYearManifactured(int yearManifactured, decimal price)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStorageByProductId(long productId, int storage)
        {
            throw new NotImplementedException();
        }
    }
}
