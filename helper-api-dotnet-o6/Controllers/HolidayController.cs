using helper_api_dotnet_o6.Models.Holiday;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HolidayController : ControllerBase
    {
        private readonly string _endPoint = "https://brasilapi.com.br/api/feriados/v1/";
        private readonly ILogger<HolidayController> _logger;
        private readonly HttpClient _httpClient;

        public HolidayController(ILogger<HolidayController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int year)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endPoint}{year}");

                if (response.IsSuccessStatusCode)
                {
                    var feriadosData = await response.Content.ReadAsStringAsync();
                    var feriadosModel = JsonConvert.DeserializeObject<List<Holiday>>(feriadosData);
                    return Ok(feriadosModel);
                }

                return BadRequest("Ano inválido ou dados não encontrados.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao consultar os feriados.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao consultar os feriados.");
            }
        }
    }
}
