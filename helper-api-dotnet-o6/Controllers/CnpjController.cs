using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using helper_api_dotnet_o6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using helper_api_dotnet_o6.Models.Cnpj;

namespace helper_api_dotnet_o6.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CnpjController : ControllerBase
    {
        private readonly string _endPoint = "https://open.cnpja.com/office";

        [HttpGet]
        [Route("/cnpj/completo/{cnpj}")]
        [ProducesResponseType(typeof(CnpjResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                    return NoContent();
                }

                return Ok(cnpjResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); 
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor.");
            }
        }

        [HttpGet]
        [Route("/cnpj/resumo/{cnpj}")]
        [ProducesResponseType(typeof(CnpjResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                    return NoContent();
                }

                return Ok(converterModelToDTO(cnpjResponse));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); 
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor.");
            }
        }

        private async Task<CnpjResponse> GetCnpjAsync(string cnpj)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync($"{_endPoint}/{cnpj}");
                return JsonSerializer.Deserialize<CnpjResponse>(response);
            }
        }
        
        //TO DO - refator para um converter elegante após a definição dos atributo do retorno com a equipe
        private CnpjDTO converterModelToDTO(CnpjResponse cnpjResponse){       

            EnderecoDto enderecoDto = new EnderecoDto (cnpjResponse.endereco.rua,cnpjResponse.endereco.numero, cnpjResponse.endereco.bairro, 
                                      cnpjResponse.endereco.cidade,  cnpjResponse.endereco.estado,  cnpjResponse.endereco.cep);
          
            CnpjDTO cnpjDto = new CnpjDTO(cnpjResponse.cnpj,cnpjResponse.empresa.nome, cnpjResponse.dataFundacao, enderecoDto );           
           
            return cnpjDto;

        }
    }
    
}