using System.Net;
using Moq;
using Moq.Protected;
using Xunit;
using helper_api_dotnet_o6.Controllers;
using helper_api_dotnet_o6.Models.Cnpj;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace helper_api_dotnet_o6.Tests.Controllers
{
    public class CnpjControllerTests
    {
        [Fact]
        public async Task ConsultaCnpjValido()
        {
            var enderecoDto = new EnderecoDto("Rua Lopes Quintas", "303","Jardim Botanico", "Rio de Janeiro", "RJ", "22460901");
            var retornoEsperado = new CnpjDTO("27865757000102", "GLOBO COMUNICACAO E PARTICIPACOES S/A", "1986-01-31", enderecoDto);
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(retornoEsperado))
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var cnpjController = new CnpjController();         
            var resultado = await cnpjController.ConsultaCnpjDto("27865757000102");     
                   
            var result =  Assert.IsAssignableFrom<ObjectResult>(resultado);
            var retornoRecebido = Assert.IsAssignableFrom<CnpjDTO>(result.Value);

            Assert.Equal(StatusCodes.Status200OK,         result.StatusCode);
            Assert.Equal(retornoRecebido.nome,            retornoEsperado.nome);
            Assert.Equal(retornoRecebido.dataFundacao,    retornoEsperado.dataFundacao);
            Assert.Equal(retornoRecebido.endereco.rua,    retornoEsperado.endereco.rua);
            Assert.Equal(retornoRecebido.endereco.numero, retornoEsperado.endereco.numero);
            Assert.Equal(retornoRecebido.endereco.bairro, retornoEsperado.endereco.bairro);
            Assert.Equal(retornoRecebido.endereco.cidade, retornoEsperado.endereco.cidade);
            Assert.Equal(retornoRecebido.endereco.estado, retornoEsperado.endereco.estado);
            Assert.Equal(retornoRecebido.endereco.cep,    retornoEsperado.endereco.cep);
   
       
        }

        [Fact]
        public async Task ConsultaCnpjInvalido()
        {
             var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

             mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var cnpjController = new CnpjController();         
            var resultado = await cnpjController.ConsultaCnpjDto("27865757000100");                   
            var result =  Assert.IsAssignableFrom<ObjectResult>(resultado);    

            Assert.Equal(StatusCodes.Status400BadRequest,         result.StatusCode);
       
        }
      
    }
}
