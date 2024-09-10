using helper_api_dotnet_o6.Models.Cnpj;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CnpjController : ControllerBase
    {
        private readonly string _endPoint = "https://brasilapi.com.br/api/cnpj/v1/";
        private readonly ILogger<CnpjController> _logger;
        private readonly HttpClient _httpClient;

        public CnpjController(ILogger<CnpjController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("{cnpj}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string cnpj)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endPoint}{cnpj}");

                if (response.IsSuccessStatusCode)
                {
                    var cnpjData = await response.Content.ReadAsStringAsync();
                    var cnpjModel = JsonConvert.DeserializeObject<CnpjData>(cnpjData);
                    return Ok(cnpjModel);
                }

                return BadRequest("CNPJ inválido ou não encontrado.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao consultar o CNPJ.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao consultar o CNPJ.");
            }
        }
    }
}
