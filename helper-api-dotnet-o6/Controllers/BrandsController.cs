using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly string _endPoint = "https://fipe.parallelum.com.br/api/v2";
        private readonly ILogger<BrandsController> _logger;

        public BrandsController(ILogger<BrandsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{vehicleType}/brands")]
        [ProducesResponseType(typeof(List<Brands>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IEnumerable<Brands>> Get(string vehicleType)
        {
            var httpHelper = new HttpRequestHelper(_endPoint);
            var brandsbyType = httpHelper.Get<List<Brands>>($"{vehicleType}/brands");
            return brandsbyType.Result;
        }
    }
}