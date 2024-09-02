using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FipeInfoController : ControllerBase
    {
        private readonly string _endPoint = "https://fipe.parallelum.com.br/api/v2";
        private readonly ILogger<FipeInfoController> _logger;

        public FipeInfoController(ILogger<FipeInfoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{vehicleType}/brands/{brand}/models/{model}/years/{modelYear}")]
        [ProducesResponseType(typeof(FipeInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<FipeInfo> Get(string vehicleType, string brand, string model, string modelYear)
        {
            var httpHelper = new HttpRequestHelper(_endPoint);
            var InfoFipe = httpHelper.Get<FipeInfo>($"{vehicleType}/brands/{brand}/models/{model}/years/{modelYear}");
            return InfoFipe.Result;
        }
    }
}