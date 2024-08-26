using helper_api_dotnet_o6.Helpers;
using Microsoft.AspNetCore.Mvc;
using helper_api_dotnet_o6.Models.Cnpj;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CnpjController : ControllerBase
    {
        private readonly string _endPoint = "https://brasilapi.com.br/api/cnpj/v1";
        private readonly ILogger<CnpjController> _logger;

        public CnpjController(ILogger<CnpjController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{cnpj}")]
        [ProducesResponseType(typeof(Root), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<Root>> Get(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14)
            {
                return BadRequest("CNPJ inválido. Deve ter exatamente 14 dígitos.");
            }

            try
            {
                var httpHelper = new HttpRequestHelper(_endPoint);
                var cnpjData = await httpHelper.Get<Root>($"{cnpj}");
                
                if (cnpjData == null || (cnpjData as Root) == null)
                {
                    return NoContent();
                }

                return Ok(cnpjData);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Erro ao buscar dados da API: {ex.Message}");
                return StatusCode(StatusCodes.Status502BadGateway, "Erro ao buscar dados da API");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro inesperado: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
            }
        }
    }
}