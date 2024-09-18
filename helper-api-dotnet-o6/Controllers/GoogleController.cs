using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Google;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoogleController : ControllerBase
    {
        private readonly string _endPoint = "https://serpapi.com";
        private readonly ILogger<GoogleController> _logger;

        public GoogleController(ILogger<GoogleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{termo}")]
        [ProducesResponseType(typeof(List<Google>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IEnumerable<OrganicResult>> Get(string termo)
        {
            var httpHelper = new HttpRequestHelper(_endPoint);
            var countries = httpHelper.Get<Google>($"search.json?q={termo}");
            return countries.Result.organic_results;
        }
    }
}