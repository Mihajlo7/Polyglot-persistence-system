using Core.ExternalData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccess.impl
{
    public  class ConsumerRepository 
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=small_database;Integrated Security=True;TrustServerCertificate=True;";

        public async Task<int> InsertConsumerBulk(List<ConsumerEx> consumers)
        {
            int count = 0;
            using var connection= new SqlConnection(connectionString);
            using var command = new SqlCommand("insertConsumer",connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            foreach (var consumer in consumers) 
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@FirstName",consumer.FirstName);
                command.Parameters.AddWithValue("@LastName", consumer.LastName);
                command.Parameters.AddWithValue("@BirthDate", consumer.BirthDay);
                command.Parameters.AddWithValue("@Email", consumer.Email);
                command.Parameters.AddWithValue("@Telephone", consumer.Telephone);
                command.Parameters.AddWithValue("@Password", consumer.Password);
                command.Parameters.AddWithValue("@CreditCardType1", consumer.CreditCardType1);
                command.Parameters.AddWithValue("@CreditCardNumber1", consumer.CreditCardNumber1);
                command.Parameters.AddWithValue("@CreditCardType2", consumer.CreditCardType2 ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CreditCardNumber2", consumer.CreditCardNumber2 ?? (object)DBNull.Value);

                
                //connection.Open();
                await command.ExecuteNonQueryAsync();
                count++;
            }
            return count;
        }
    }
}
