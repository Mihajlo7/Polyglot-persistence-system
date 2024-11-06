using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace IDataAccess
{
    public interface IConsumerRepository
    {
        public Task InsertOneConsumer(ConsumerModel consumer);
        public Task<int> InsertManyConsumer(List<ConsumerModel> consumers);
        public Task InsertOneFriend(ConsumerFriendModel consumerFriend);
        public Task<int> InsertManyFriend(List<ConsumerFriendModel> consumerFriends);
        public Task InsertManyFriendBulk(List<ConsumerFriendModel> consumerFriends);
        public Task<bool> DeleteConsumerById(long consumerId);
        public Task<bool> DeleteConsumers();
        public Task<bool> DeleteConsumersFriends();
    }
}
