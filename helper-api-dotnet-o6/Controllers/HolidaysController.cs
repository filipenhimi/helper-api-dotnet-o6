using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Holidays;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HolidaysController : ControllerBase
    {
        private readonly string _endPoint = "https://brasilapi.com.br/api/";
        private readonly ILogger<HolidaysController> _logger;

        public HolidaysController(ILogger<HolidaysController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{ano}")]
        [ProducesResponseType(typeof(List<Holidays>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IEnumerable<Holidays>> Get(string ano)
        {
            var httpHelper = new HttpRequestHelper(_endPoint);
            var countries = httpHelper.Get<List<Holidays>>($"feriados/v1/{ano}");
            return countries.Result;
        }
    }
}