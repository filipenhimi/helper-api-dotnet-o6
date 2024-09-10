using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.CnpjModel;
using Microsoft.AspNetCore.Mvc;


namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CnpjController : ControllerBase
    {
        private readonly string _endPoint = "https://brasilapi.com.br/api/cnpj/v1/";

        public CnpjController(ILogger<CnpjController> logger, HttpClient httpClient)
        {}

        [HttpGet]
        [Route("{cnpj}")]
        [ProducesResponseType(typeof(CnpjModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<CnpjModel> Get(string cnpj)
        {
            var httpHelper = new HttpRequestHelper(_endPoint);
            var response = httpHelper.Get<CnpjModel>($"{cnpj}");
            return response.Result;

        }
    }
}