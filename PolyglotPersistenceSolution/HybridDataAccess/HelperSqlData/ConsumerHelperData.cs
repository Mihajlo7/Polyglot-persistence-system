using Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HybridDataAccess.HelperSqlData
{
    internal static class ConsumerHelperData
    {
        public static SqlCommand CreateConsumerCommand(this SqlCommand command, ConsumerModel consumer)
        {
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Id", consumer.Id);
            command.Parameters.AddWithValue("@FirstName", consumer.FirstName);
            command.Parameters.AddWithValue("@LastName", consumer.LastName);
            command.Parameters.AddWithValue("@BirthDate", consumer.BirthDate);
            command.Parameters.AddWithValue("@Email", consumer.Email);
            command.Parameters.AddWithValue("@Telephone", consumer.Telephone);
            command.Parameters.AddWithValue("@Password", consumer.Password);
            command.Parameters.AddWithValue("@CreditCard1Id", consumer.CreditCards[0].Id);
            command.Parameters.AddWithValue("@CreditCardType1", consumer.CreditCards[0].CardType);
            command.Parameters.AddWithValue("@CreditCardNumber1", consumer.CreditCards[0].Number);
            command.Parameters.AddWithValue("@CreditCard2Id", consumer.CreditCards[1].Id);
            command.Parameters.AddWithValue("@CreditCardType2", consumer.CreditCards[1].CardType ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CreditCardNumber2", consumer.CreditCards[1].Number ?? (object)DBNull.Value);

            return command;
        }

        public static SqlCommand CreateConsumerFriendCommand(this SqlCommand command, ConsumerModel consumerFriend)
        {
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ConsumerId", consumerFriend.Id);
            command.Parameters.AddWithValue("@FriendsData", JsonSerializer.Serialize(consumerFriend.Friends));

            return command;
        }
    }
}
