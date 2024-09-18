
using Microsoft.AspNetCore.Mvc;
using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Feriados;

namespace helper_api_dotnet_o6.Controllers
{
    // Controlador responsável por lidar com requisições relacionadas aos feriados.
    // Ele utiliza a BrasilAPI para buscar feriados de um ano específico e expõe um endpoint GET.
    [ApiController]
    [Route("[controller]")]
    public class FeriadosController : ControllerBase
    {
        // URL base do serviço externo (BrasilAPI) utilizado para buscar informações de feriados.
        private readonly string _endPoint = "https://brasilapi.com.br/api/feriados/v1";
        private readonly ILogger<FeriadosController> _logger;
         public FeriadosController(ILogger<FeriadosController> logger)
        {
            _logger = logger;
        }

        // Método GET para buscar feriados de um ano específico.        
        // Entrada:
        // - string ano: O ano para o qual os feriados serão buscados.
        //
        // Retornos:
        // - 200 OK: Retorna uma lista de feriados se os dados forem encontrados.
        // - 204 No Content: Se não houver feriados para o ano ou se os dados estiverem nulos ou vazios.
        // - 502 Bad Gateway: Se houver um erro ao se comunicar com o serviço externo.
        // - 500 Internal Server Error: Para erros inesperados no servidor.
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
                // Instancia um HttpRequestHelper para fazer a chamada à BrasilAP
                var httpHelper = new HttpRequestHelper(_endPoint);

                // Faz a requisição GET à API para buscar os feriados de um ano específico.
                var feriados = await httpHelper.Get<List<FeriadosModel>>($"{ano}");

                // Se a lista de feriados for nula ou vazia, retorna No Content (204).
                if (feriados == null || !feriados.Any())
                {
                    return NoContent();
                }

                // Retorna a lista de feriados com status 200 (OK).
                return Ok(feriados);
            }
            catch (HttpRequestException ex)
            {
                // Captura erros de requisição HTTP, como falha ao conectar à API externa (BrasilAPI).
                _logger.LogError(ex, "Erro ao tentar se conectar à API externa.");

                // Retorna um status 502 (Bad Gateway) indicando falha na comunicação com o serviço externo.
                return StatusCode(StatusCodes.Status502BadGateway, "Erro ao se comunicar com o serviço externo.");
            }
            catch (Exception ex)
            {
                // Captura qualquer outra exceção inesperada.
                _logger.LogError(ex, "Erro interno no servidor.");

                // Retorna um status 500 (Internal Server Error) indicando um erro genérico no servidor.
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor.");
            }
        }
    }
}