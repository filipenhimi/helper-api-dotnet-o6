using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using helper_api_dotnet_o6.Models.Cep;
using helper_api_dotnet_o6.Models.Cnpj;
using helper_api_dotnet_o6.Models;
using System.Diagnostics;

namespace helper_api_dotnet_o6.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CnpjController : ControllerBase
    {
        private readonly string _endPoint = "https://open.cnpja.com/office";
        private const int Status429LimiteMinutiExcedido = 429;

        [HttpGet]
        [Route("/cnpj/completo/{cnpj}")]
        [ProducesResponseType(typeof(CnpjDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> ConsultaCnpj(string cnpj)
        {
           try
            {
                CnpjResponse cnpjResponse = await GetCnpjAsync(cnpj);

                if (cnpjResponse == null)
                {
                    return NotFound("CNPJ não encontrado.");
                }

                return Ok(converterModelToDTO(cnpjResponse));
            }
            catch (HttpRequestException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
               return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor.");
            }
        }

        [HttpGet]
        [Route("/cnpj/resumo/{cnpj}")]
        [ProducesResponseType(typeof(CnpjDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> ConsultaCnpjDto(string cnpj)
        {
            try
            {
                CnpjResponse cnpjResponse = await GetCnpjAsync(cnpj);

                if (cnpjResponse == null)
                {
                    return NotFound("CNPJ não encontrado.");
                }

                return Ok(converterModelToDTO(cnpjResponse));
            }
            catch (HttpRequestException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
               return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor.");
            }
        }

        private async Task<Models.CnpjResponse> GetCnpjAsync(string cnpj)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{_endPoint}/{cnpj}");

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException("Parâmetro de consulta mal formatado ou faltante.");
                }

                else if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new HttpRequestException("Créditos esgotados ou limite por minuto excedido.");
                }
                
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new HttpRequestException("CNPJ não registrado na Receita Federal.");

                }
                var responseString = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<CnpjResponse>(responseString);
            }
        }
        
        //TO DO - refator para um converter elegante após a definição dos atributo do retorno com a equipe
        private CnpjDTO converterModelToDTO(CnpjResponse cnpjResponse){       

            EnderecoDto enderecoDto = new EnderecoDto (cnpjResponse.endereco.rua,cnpjResponse.endereco.numero, cnpjResponse.endereco.bairro, 
                                      cnpjResponse.endereco.cidade,  cnpjResponse.endereco.estado,  cnpjResponse.endereco.cep);
          
            CnpjDTO cnpjDto = new CnpjDTO(cnpjResponse.cnpj,cnpjResponse.empresa.nome, cnpjResponse.dataFundacao, enderecoDto );           
           
            return cnpjDto;

        }

        internal async Task ConsultaCnpjDto()
        {
            throw new NotImplementedException();
        }
    }
    
}