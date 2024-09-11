using helper_api_dotnet_o6.Helpers;
using Microsoft.AspNetCore.Mvc;
using helper_api_dotnet_o6.Models.Cnpj;

namespace helper_api_dotnet_o6.Controllers
{
    // Controlador responsável por lidar com requisições relacionadas a CNPJs.
    // Ele expõe um endpoint GET para buscar detalhes de um CNPJ específico.
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

        // Método GET para buscar detalhes de um CNPJ.
        //  Entrada:
        // - string cnpj: O CNPJ a ser buscado (deve ter 14 dígitos).
        //
        // Retornos:
        // - 200 OK: Se o CNPJ for encontrado, retorna os dados como um objeto do tipo Root.
        // - 204 No Content: Se o CNPJ não for encontrado ou os dados forem nulos.
        // - 400 Bad Request: Se o CNPJ fornecido for inválido (não possuir 14 dígitos).
        // - 502 Bad Gateway: Se houver um erro ao se conectar à API externa.
        // - 500 Internal Server Error: Para erros inesperados no servidor.
        [HttpGet]
        [Route("{cnpj}")]
        [ProducesResponseType(typeof(Root), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<Root>> Get(string cnpj)
        {
            // Validação básica do CNPJ. Verifica se o CNPJ é nulo ou vazio, e se ele tem exatamente 14 dígitos.
            if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14)
            {
                return BadRequest("CNPJ inválido. Deve ter exatamente 14 dígitos.");
            }

            try
            {
                // Cria uma instância de HttpRequestHelper para realizar a chamada à BrasilAPI.
                var httpHelper = new HttpRequestHelper(_endPoint);

                // Faz a requisição GET à API para buscar os dados do CNPJ.
                var cnpjData = await httpHelper.Get<Root>($"{cnpj}");

                // Se não houver dados retornados ou se o objeto for nulo, retorna No Content (204).
                if (cnpjData == null || (cnpjData as Root) == null)
                {
                    return NoContent();
                }

                // Retorna o objeto com status 200 (Ok) se a chamada for bem-sucedida e os dados forem válidos.
                return Ok(cnpjData);
            }
            catch (HttpRequestException ex)
            {
                // Captura erros de requisição HTTP, como falha ao conectar à API externa (BrasilAPI).
                _logger.LogError($"Erro ao buscar dados da API: {ex.Message}");

                // Retorna um status 502 (Bad Gateway) indicando falha na comunicação com o serviço externo.
                return StatusCode(StatusCodes.Status502BadGateway, "Erro ao buscar dados da API");
            }
            catch (Exception ex)
            {
                // Captura qualquer outra exceção inesperada.
                _logger.LogError($"Erro inesperado: {ex.Message}");

                // Retorna um status 500 (Internal Server Error) indicando um erro genérico no servidor.
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
            }
        }
    }
}