using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleModelsController : ControllerBase
    {
        private readonly string _endPoint = "https://fipe.parallelum.com.br/api/v2";
        private readonly ILogger<VehicleModelsController> _logger;

        public VehicleModelsController(ILogger<VehicleModelsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{vehicleType}/brands/{brand}")]
        [ProducesResponseType(typeof(List<VehicleModels>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IEnumerable<VehicleModels>> Get(string vehicleType, string brand)
        {
            var httpHelper = new HttpRequestHelper(_endPoint);
            var VehicleByBrand = httpHelper.Get<List<VehicleModels>>($"{vehicleType}/brands/{brand}/models");
            return VehicleByBrand.Result;
        }
    }
}