using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YearsController : ControllerBase
    {
        private readonly string _endPoint = "https://fipe.parallelum.com.br/api/v2";
        private readonly ILogger<YearsController> _logger;

        public YearsController(ILogger<YearsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{vehicleType}/brands/{brand}/models/{model}/years")]
        [ProducesResponseType(typeof(List<Years>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IEnumerable<Years>> Get(string vehicleType, string brand, string model)
        {
            var httpHelper = new HttpRequestHelper(_endPoint);
            var fipeInfobyYear = httpHelper.Get<List<Years>>($"{vehicleType}/brands/{brand}/models/{model}/years");
            return fipeInfobyYear.Result;
        }
    }
}