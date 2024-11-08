using Core.Models;
using HybridDataAccess.HelperSqlData;
using IDataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridDataAccess.Implementation
{
    public class HybridConsumerRepository : IConsumerRepository
    {
        private readonly string _connectionString;
        private readonly string _database;

        public HybridConsumerRepository(string database)
        {
            _database = database;
            _connectionString = $"Data Source=.;Initial Catalog={database};Integrated Security=True;TrustServerCertificate=True;";
        }

        public async Task<bool> DeleteConsumerById(long consumerId)
        {
            string query = "DELETE FROM Consumers WHERE Id=@consumerId;";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@consumerId",consumerId);

            connection.Open();
            var res = await command.ExecuteNonQueryAsync();

            return res > 0;
        }

        public async Task<bool> DeleteConsumers()
        {
            string query = "DELETE FROM Consumers;";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            connection.Open();
            var res = await command.ExecuteNonQueryAsync();

            return res > 0;
        }

        public async Task<bool> DeleteConsumersFriends()
        {
            string query = "UPDATE Consumers SET friends=NULL;";

            using var connection = new SqlConnection( _connectionString );
            using var command = new SqlCommand( query, connection );

            connection.Open();
            var res = await command.ExecuteNonQueryAsync();

            return res > 0;
        }

        public Task<ConsumerModel> GetConsumerByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ConsumerModel> GetConsumerById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ConsumerModel>> GetConsumers()
        {
            throw new NotImplementedException();
        }

        public Task<List<ConsumerModel>> GetConsumersByEmailAndFriendshipLevel(string email, int level)
        {
            throw new NotImplementedException();
        }

        public Task<List<ConsumerModel>> GetConsumersByFriendEmail(string friendEmail)
        {
            throw new NotImplementedException();
        }

        public Task<List<ConsumerModel>> GetConsumersOptimised()
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertManyConsumer(List<ConsumerModel> consumers)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("CreateUser", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

            int count = 0;
            foreach (var consumer in consumers)
            {
                command.CreateConsumerCommand(consumer);
                await command.ExecuteNonQueryAsync();
                count++;
            }

            return count;
        }

        public async Task<int> InsertManyFriend(List<ConsumerModel> consumerFriends)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("CreateFriendsJson", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            int count = 0;
            connection.Open();

            foreach (var consumer in consumerFriends)
            {
                command.CreateConsumerFriendCommand(consumer);
                await command.ExecuteNonQueryAsync();
                count++;
            }
            return count;
        }

        public async Task InsertManyFriendBulk(List<ConsumerModel> consumerFriends)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("CreateFriendsJson", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();

            foreach (var consumer in consumerFriends) 
            {
                command.CreateConsumerFriendCommand(consumer);
                await command.ExecuteNonQueryAsync();
                
            }
        }

        public async Task InsertOneConsumer(ConsumerModel consumer)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("CreateUser", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();
            command.CreateConsumerCommand(consumer);

            await command.ExecuteNonQueryAsync();
        }

        public async Task InsertOneFriend(ConsumerModel consumerFriend)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("CreateFriendsJson", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();
            command.CreateConsumerFriendCommand(consumerFriend);

            await command.ExecuteNonQueryAsync();
        }

        public Task UpdateConsumersByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateConsumerTelephoneByEmail(string email, string telephone)
        {
            throw new NotImplementedException();
        }

        public Task UpdateConsumerTelephoneById(long consumerId, string telephone)
        {
            throw new NotImplementedException();
        }
    }
}
