using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Country;
using helper_api_dotnet_o6_investimento.Domain.Interfaces;
using helper_api_dotnet_o6_investimento.Domain.Request;
using helper_api_dotnet_o6_investimento.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestimentoController : ControllerBase
    {
        //private readonly string _endPoint = "https://servicodados.ibge.gov.br/api/v1";
        //private readonly ILogger<CountryController> _logger;
        private readonly IInvestimentoService _service;
        public InvestimentoController(IInvestimentoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("v1/obter-taxas")]
        [ProducesResponseType(typeof(ObterCdiAtualResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObterCdiAtual());
        }

        //[HttpPost]
        //[Route("v1/calcular-investimento")]
        //[ProducesResponseType(typeof(CalcularInvestimentoResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status502BadGateway)]
        //public async Task<IActionResult> CalcularInvestimento(CalcularInvestimentoRequest request)
        //{
        //    return Ok(await _service.CalcularProvisaoInvestimento(request));
        //}

        [HttpPost]
        [Route("v1/calcular-investimentos")]
        [ProducesResponseType(typeof(CalcularInvestimentoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> CalcularInvestimentos(CalcularInvestimentoRequest request)
        {
            return Ok(await _service.CalcularProvisaoInvestimentos(request));
        }
    }
}