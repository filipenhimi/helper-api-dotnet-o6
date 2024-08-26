
using Microsoft.AspNetCore.Mvc;
using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Feriados;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeriadosController : ControllerBase
    {
        private readonly string _endPoint = "https://brasilapi.com.br/api/feriados/v1";
        private readonly ILogger<FeriadosController> _logger;
         public FeriadosController(ILogger<FeriadosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{ano}")]
        [ProducesResponseType(typeof(List<FeriadosModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
         public async Task<IActionResult> Get(string ano)
        {
            try
            {
                var httpHelper = new HttpRequestHelper(_endPoint);
                var feriados = await httpHelper.Get<List<FeriadosModel>>($"{ano}");

                if (feriados == null || !feriados.Any())
                {
                    return NoContent();
                }

                return Ok(feriados);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao tentar se conectar à API externa.");
                return StatusCode(StatusCodes.Status502BadGateway, "Erro ao se comunicar com o serviço externo.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno no servidor.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor.");
            }
        }

    }
}