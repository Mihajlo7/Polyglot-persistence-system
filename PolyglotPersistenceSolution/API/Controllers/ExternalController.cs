using API.Services;
using Core.ExternalData;
using Microsoft.AspNetCore.Mvc;
using SQLDataAccess.impl;

namespace API.Controllers
{
    [ApiController]
    [Route("api/external/generate")]
    public class ExternalController : ControllerBase
    {
        private readonly IHttpClientFactoryService _httpClientFactoryService;

        public ExternalController(IHttpClientFactoryService httpClientFactoryService)
        {
            _httpClientFactoryService = httpClientFactoryService;
        }

        [HttpGet("consumers")]
        public async Task<IActionResult> GetAllConsumers() 
        {
            var result = await _httpClientFactoryService.ExecuteAsync<ConsumerEx>("consumers",1);
            ConsumerRepository consumerRepository = new();
            int res= await consumerRepository.InsertConsumerBulk(result);
            return Ok(res);
        }
    }
}
