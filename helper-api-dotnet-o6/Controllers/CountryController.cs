using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Country;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly string _endPoint = "https://servicodados.ibge.gov.br/api/v1";
        private readonly ILogger<CountryController> _logger;

        public CountryController(ILogger<CountryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{siglas}")]
        [ProducesResponseType(typeof(List<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IEnumerable<Country>> Get(string siglas)
        {
            var httpHelper = new HttpRequestHelper(_endPoint);
            var countries = httpHelper.Get<List<Country>>($"paises/{siglas}");
            return countries.Result;
        }
    }
}