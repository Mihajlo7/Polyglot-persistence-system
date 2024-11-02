using Core.Models;
using Microsoft.Data.SqlClient;
using MongoDB.Driver;
using SQLDataAccess.HelperSqlData;
using SQLDataAccess.Resources;
using System.Data;

namespace SQLDataAccess.impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=small_database;Integrated Security=True;TrustServerCertificate=True;";

        public async Task<List<ProductModel>> GetAllProductsWithCompaniesBySelect()
        {
            var query = ProductsQueries.GetProductsByJoinPlain;

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            command.CommandTimeout = 120;

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();

            var products = ProductSqlDataHelper.CreateProductsWithCompanyBadWay(reader);

            return products;
        }

        public async Task<List<ProductModel>> GetAllProductsWithCompaniesBySelectOptimised()
        {
            var query= ProductsQueries.GetProductsByJoinOptimised;

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            command.CommandTimeout = 120;

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();

            var products = ProductSqlDataHelper.CreateProductsWithCompany(reader);
            return products;
        }

        public async Task<List<ProductModel>> GetAllProductsWithCompaniesBySelectSubQuery()
        {
            var query = ProductsQueries.GetProductsBySubQuery;
            using var connection= new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();
            var products = ProductSqlDataHelper.CreateProductsWithCompany(reader);

            return products;
        }

        public async Task<List<ProductModel>> GetAllProductsWithCompaniesBySubQueryApply()
        {
            var query = ProductsQueries.GetProductsBySubQueryWithApply;
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();
            var products = ProductSqlDataHelper.CreateProductsWithCompany(reader);

            return products;
        }

        public async Task<int> InsertMany(List<ProductModel> products)
        {
            int count = 0;
            foreach (var product in products) 
            {
                await InsertOne(product);
                count++;
            }
            return count;
        }

        public async Task<int> InsertOne(ProductModel product)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("usp_InsertProduct", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            var categoryName = product.SubCategory.Category.Name;

            connection.Open();

            command.Parameters.AddWithValue("@ProductName",product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@SubCategoryName", product.SubCategory.Name);
            command.Parameters.AddWithValue("@ProducedBy", product.Produced.Id);
            if (product.Store!=null)
            {
                command.Parameters.AddWithValue("@StoreId", product.Store.Id);
            }
            command.Parameters.AddWithValue("@ShortDescription", product.Details.ShortDescription);
            command.Parameters.AddWithValue("@ImageUrl", product.Details.ImageUrl);
            command.Parameters.AddWithValue("@ProductType", product.SubCategory.Category.Name);

            var distributeCompanies = new DataTable();
            distributeCompanies.Columns.Add("SellerId",typeof(long));
            distributeCompanies.Columns.Add("Price", typeof(decimal));

            foreach(var dc in product.Distribute)
            {
                distributeCompanies.Rows.Add(dc.Distributor.Id,dc.DistributionPrice);
            }
            command.Parameters.AddWithValue("@DistributeProducts", distributeCompanies);

            if (categoryName=="Car")
            {
                CarDetailsModel carDetails= product.Details as CarDetailsModel;
                command.Parameters.AddWithValue("@YearManufactured", carDetails.YearManufactured);
                command.Parameters.AddWithValue("@SerialNumber", carDetails.SerialNumber);
                command.Parameters.AddWithValue("@EngineDisplacement", carDetails.EngineDisplacement);
                command.Parameters.AddWithValue("@EnginePower", carDetails.EnginePower);
                command.Parameters.AddWithValue("@LongDescription", carDetails.LongDescription);
                command.Parameters.AddWithValue("@CarModel", product.SubCategory.Name);

            } else if (categoryName == "Mobile")
            {
                MobileDetailsModel mobileDetails= product.Details as MobileDetailsModel;
                command.Parameters.AddWithValue("@YearManufactured", mobileDetails.YearManufactured);
                command.Parameters.AddWithValue("@SerialNumber", mobileDetails.SerialNumber);
                command.Parameters.AddWithValue("@Weight", mobileDetails.Weight);
                command.Parameters.AddWithValue("@Storage", mobileDetails.Storage);
                command.Parameters.AddWithValue("@ScreenDiagonal", mobileDetails.ScreenDiagonal);
                command.Parameters.AddWithValue("@OperatingSystem", mobileDetails.OperatingSystem);
                command.Parameters.AddWithValue("@Color", mobileDetails.Color);
            }

            int affectedRow= await command.ExecuteNonQueryAsync();
            return affectedRow;

        }
    }
}
