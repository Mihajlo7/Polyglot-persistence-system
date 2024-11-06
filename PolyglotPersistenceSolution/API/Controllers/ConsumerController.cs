﻿using Core.Models;
using IDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerRepository _consumerRepository;

        public ConsumerController(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertConsumer([FromBody] ConsumerModel consumer)
        {
            await _consumerRepository.InsertOneConsumer(consumer);
            return Ok("Inserted 1 consumer");
        }
        [HttpPost("insertMany")]
        public async Task<IActionResult> InsertManyConsumers([FromBody] List<ConsumerModel> consumers)
        {
            int count = await _consumerRepository.InsertManyConsumer(consumers);
            return Ok($"Inserted {count} consumers");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllConsumers()
        {
            bool success = await _consumerRepository.DeleteConsumers();
            if (success) 
            {
                return Ok("Delleted all consumers");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost("friends/insert")]
        public async Task<IActionResult> InsertConsumerFriend([FromBody]ConsumerFriendModel consumerFriend) 
        {
            await _consumerRepository.InsertOneFriend(consumerFriend);
            return Ok("Inserted consumer Friends");
        }

        [HttpPost("friends/insertMany")]
        public async Task<IActionResult> InsertConsumerFriends([FromBody]List<ConsumerFriendModel> consumerFriends)
        {
            int count= await _consumerRepository.InsertManyFriend(consumerFriends);
            return Ok($"Inserted {count} consumer Friends");
        }

        [HttpPost("friends/insertBulk")]
        public async Task<IActionResult> InsertConsumerFriendsBulk([FromBody] List<ConsumerFriendModel> consumerFriends)
        {
            await _consumerRepository.InsertManyFriendBulk(consumerFriends);
            return Ok($"Inserted consumer Friends Bulk");
        }

        [HttpDelete("friends")]
        public async Task<IActionResult> DeleteConsumersFriends()
        {
            bool success = await _consumerRepository.DeleteConsumersFriends();
            if (success)
            {
                return Ok("Delleted all consumers friends");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}