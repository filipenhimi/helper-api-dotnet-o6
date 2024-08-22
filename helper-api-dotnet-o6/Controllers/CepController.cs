using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using helper_api_dotnet_o6.Models.Cep;


namespace helper_api_dotnet_o6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CepController : ControllerBase
    {
        private readonly string _endPoint = "https://brasilapi.com.br/api/cep/v1";

        [HttpGet]
        [Route("{cep}")]
        [ProducesResponseType(typeof(CepResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> ConsultaCep(string cep)
        {
            try
            {
                var cepResponse = await GetCepAsync(cep);

                if (cepResponse == null)
                {
                    return NoContent();
                }

                return Ok(cepResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); 
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor.");
            }
        }

        private async Task<CepResponse> GetCepAsync(string cep)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync($"{_endPoint}/{cep}");
                return JsonSerializer.Deserialize<CepResponse>(response);
            }
        }
    }
}


