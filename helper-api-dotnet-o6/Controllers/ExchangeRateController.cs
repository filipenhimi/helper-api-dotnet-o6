using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Exchange;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly string _endPoint = "https://economia.awesomeapi.com.br/last";
        private readonly ILogger<ExchangeRateController> _logger;

        public ExchangeRateController(ILogger<ExchangeRateController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{fromCurrency}-{toCurrency}")]
        [ProducesResponseType(typeof(ExchangeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string fromCurrency, string toCurrency)
        {
            var httpHelper = new HttpRequestHelper(_endPoint);
            var getResult = await httpHelper.Get<ExchangeResponse>($"{fromCurrency}-{toCurrency}");

            if (getResult != null && getResult.Exchanges != null)
            {
                var exchanges = getResult.Exchanges.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.ToObject<CurrencyExchange>()
                );

                return Ok(exchanges);
            }

            return NoContent();
        }
    }
}
