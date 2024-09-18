using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Bank;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
        private readonly string _endPoint = "https://brasilapi.com.br/api";
        private readonly ILogger<BankController> _logger;

        public BankController(ILogger<BankController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{nome}")]
        [ProducesResponseType(typeof(List<Bank>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IEnumerable<Bank>> Get(string nome)
        {
            var httpHelper = new HttpRequestHelper(_endPoint);
            var banks = await httpHelper.Get<List<Bank>>($"banks/v1");
            // Filtrar os bancos pelo nome fornecido
            var filteredBanks = banks
                .Where(b => !string.IsNullOrEmpty(b.name) && b.name.ToLower().Contains(nome.ToLower()))
                .ToList();
            return filteredBanks;
        }
    }
}