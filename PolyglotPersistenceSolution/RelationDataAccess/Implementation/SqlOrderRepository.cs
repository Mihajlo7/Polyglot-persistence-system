using Core.Models;
using IDataAccess;
using Microsoft.Data.SqlClient;
using RelationDataAccess.HelperSqlData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationDataAccess.Implementation
{
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly string _database;
        private readonly string _connectionString;

        public SqlOrderRepository(string database)
        {
            _database = database;
            _connectionString = $"Data Source=.;Initial Catalog={_database};Integrated Security=True;TrustServerCertificate=True;";
        }

        public Task AddProduct(ProductModel product, ChartModel chart)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCharts()
        {
            string query = "DELETE FROM Charts";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            connection.Open();
            int res = await command.ExecuteNonQueryAsync();

            return res > 0;
        }

        public Task<bool> DeleteChartsByConsumerId(long consumerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(ProductModel product, ChartModel chart)
        {
            throw new NotImplementedException();
        }

        public Task<ChartModel> GetAllChartsBadWay(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public async Task InsertChart(ChartModel chart)
        {
            string query = "CreateChart";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.CommandType=System.Data.CommandType.StoredProcedure;
            command.CreateInsertChartCommand(chart);

            connection.Open();

            await command.ExecuteNonQueryAsync();
        }

        public Task<int> InsertManyOrder(List<OrderModel> orders)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertManyOrderBulk(List<OrderModel> orders)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneOrder(OrderModel order)
        {
            throw new NotImplementedException();
        }

        public Task UpdateChart(ChartModel chart)
        {
            throw new NotImplementedException();
        }
    }
}
