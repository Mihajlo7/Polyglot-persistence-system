using API.Services;
using Core.ExternalData;
using Microsoft.AspNetCore.Mvc;
using Services;
using SQLDataAccess;
using SQLDataAccess.impl;

namespace API.Controllers
{
    [ApiController]
    [Route("api/external/generate")]
    public class ExternalController : ControllerBase
    {
        private readonly IHttpClientFactoryService _httpClientFactoryService;
        private readonly IConsumerRepository _consumerRepository;

        public ExternalController(IHttpClientFactoryService httpClientFactoryService,IConsumerRepository consumerRepository)
        {
            _httpClientFactoryService = httpClientFactoryService;
            _consumerRepository = consumerRepository;
        }

        [HttpGet("consumers")]
        public async Task<IActionResult> GetAllConsumers() 
        {
            
            var result = await _httpClientFactoryService.ExecuteAsync<ConsumerEx>("consumers",4);
            ConsumerRepository consumerRepository = new();
            int res= await consumerRepository.InsertConsumerBulk(result);
            return Ok(res);
            
            //return Ok(await _consumerRepository.GetAllBySelect());
        }
        [HttpGet("sellers")]
        public async Task<IActionResult> GetAllSellers()
        {
            var sellers = await _httpClientFactoryService.ExecuteAsync<SellerEx>("sellers", 4);
            CompanyRepository companyRepository = new();
            int res= await companyRepository.InsertManySellers(sellers);
            return Ok(res);
        }

        [HttpGet("cars")]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _httpClientFactoryService.ExecuteAsync<CarEx>("cars", 1);
            CompanyRepository companyRepository = new();
            var sellers= await companyRepository.GetAllSellersBySelect();
            return Ok(ProductGeneratorService.GenerateProductsList(sellers,cars));
        }
        [HttpGet("mobiles")]
        public async Task<IActionResult> GetAllMobiles()
        {
            var mobiles = await _httpClientFactoryService.ExecuteAsync<MobileEx>("mobiles", 1);
            return Ok(mobiles);
        }

        [HttpGet("couriers")]
        public async Task<IActionResult> GetAllCouriers()
        {
            var couriers = await _httpClientFactoryService.ExecuteAsync<CourierEx>("couriers", 1);
            CompanyRepository companyRepository = new();
            var res=await companyRepository.InsertManyCouriers(couriers);
            return Ok(res);
        }

        [HttpGet("contracts")]
        public async Task<IActionResult> GetAllContracts()
        {
            CompanyRepository companyRepository = new();
            var couriers= await companyRepository.GetAllCouriersBySelect();
            var companies = await companyRepository.GetAllCompaniesBySelect();

            var results = CompanyGeneratorService.GenerateContractsForCourier(couriers, companies);

            return Ok(results);
        }

        
    }
}
