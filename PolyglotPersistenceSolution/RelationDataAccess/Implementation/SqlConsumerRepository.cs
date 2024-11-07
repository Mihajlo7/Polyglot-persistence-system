using Core.Models;
using IDataAccess;
using Microsoft.Data.SqlClient;
using RelationDataAccess.HelperSqlData;
using RelationDataAccess.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationDataAccess.Implementation
{
    public class SqlConsumerRepository : IConsumerRepository
    {
        private readonly string _connectionString;
        private readonly string _database;

        public SqlConsumerRepository(string database)
        {
            _database = database;
            _connectionString = $"Data Source=.;Initial Catalog={database};Integrated Security=True;TrustServerCertificate=True;";
        }

        public Task<bool> DeleteConsumerById(long consumerId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteConsumers()
        {
            string query = "DELETE FROM Users";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            connection.Open();
            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteConsumersFriends()
        {
            string query = "DELETE FROM ConsumerFriends;";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            connection.Open();
            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<ConsumerModel> GetConsumerByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(ConsumerQueries.GetByEmail, connection);
            command.Parameters.AddWithValue("@Email", email);

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();
            var consumer = reader.GetConsumersFromReader().First();

            return consumer;
        }

        public async Task<ConsumerModel> GetConsumerById(long id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(ConsumerQueries.GetById, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();
            var consumer=reader.GetConsumersFromReader().First();

            return consumer;
        }

        public async Task<List<ConsumerModel>> GetConsumers()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(ConsumerQueries.JoinPlain, connection);
            command.CommandTimeout = 120;

            connection.Open();
            using var reader= await command.ExecuteReaderAsync();

            var consumers=reader.GetConsumersFromReaderBadWay();

            return consumers;
        }

        public async Task<List<ConsumerModel>> GetConsumersByEmailAndFriendshipLevel(string email, int level)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(ConsumerQueries.GetConsumersByEmailAndFriendshipLevel, connection);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@FriendshipLevel",level);

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();

            var consumers = reader.GetConsumersFromReader();
            return consumers;
        }

        public async Task<List<ConsumerModel>> GetConsumersByFriendEmail(string friendEmail)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(ConsumerQueries.GetConsumerByFriendEmail, connection);
            command.Parameters.AddWithValue("@Email", friendEmail);

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();

            var consumers= reader.GetConsumersFromReader();
            return consumers;

        }

        public async Task<List<ConsumerModel>> GetConsumersOptimised()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(ConsumerQueries.JoinWithNames, connection);

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();

            var consumers = reader.GetConsumersFromReader();

            return consumers;
        }

        public async Task<int> InsertManyConsumer(List<ConsumerModel> consumers)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("CreateUser", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

            int count = 0;
            foreach(var consumer in consumers)
            {
                command.CreateConsumerCommand(consumer);
                await command.ExecuteNonQueryAsync();
                count++;
            }

            return count;
        }

        public async Task<int> InsertManyFriend(List<ConsumerFriendModel> consumerFriends)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("CreateConsumerFriends", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();
            int count = 0;
            foreach (var consumerFriend in consumerFriends)
            {
                command.CreateConsumerFriendCommand(consumerFriend);
                await command.ExecuteNonQueryAsync();
                count++;
            }

            return count;
        }

        public async Task InsertManyFriendBulk(List<ConsumerFriendModel> consumerFriends)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("CreateConsumerFriendsBulk", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();
            command.CreateConsumerFriendBulkCommand(consumerFriends);

            await command.ExecuteNonQueryAsync();
            
        }

        public async Task InsertOneConsumer(ConsumerModel consumer)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("CreateUser",connection);
            command.CommandType=System.Data.CommandType.StoredProcedure;

            connection.Open();
            command.CreateConsumerCommand(consumer);

            await command.ExecuteNonQueryAsync();
        }

        public async Task InsertOneFriend(ConsumerFriendModel consumerFriend)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("CreateConsumerFriends", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();
            command.CreateConsumerFriendCommand(consumerFriend);

            await command.ExecuteNonQueryAsync();
        }
    }
}
