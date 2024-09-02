using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using helper_api_dotnet_o6.Controllers;
using helper_api_dotnet_o6.Models.Cep;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Xunit;

namespace helper_api_dotnet_o6.Tests.Controllers
{
    public class CepControllerTests
    {
        [Fact]
        public async Task ConsultaCep_ReturnsOk_WithValidCep()
        {
            var cep = "01001000";
            var expectedResponse = new CepResponse();
            expectedResponse.Cep = "01001000";
            expectedResponse.City = "São Paulo";
            expectedResponse.Neighborhood = "Sé";
            expectedResponse.State = "SP";
            expectedResponse.Street = "Praça da Sé";
            expectedResponse.Service = "open-cep";
            
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(expectedResponse))
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var controller = new CepController();

            var result = await controller.ConsultaCep(cep);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<CepResponse>(okResult.Value);

            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(expectedResponse.Cep, actualResponse.Cep);
            Assert.Equal(expectedResponse.City, actualResponse.City);
            Assert.Equal(expectedResponse.Neighborhood, actualResponse.Neighborhood);
            Assert.Equal(expectedResponse.State, actualResponse.State);
            Assert.Equal(expectedResponse.Street, actualResponse.Street);
            Assert.Equal(expectedResponse.Service, actualResponse.Service);
        }

        [Fact]
        public async Task ConsultaCep_ReturnsNotFound_WithNotFoundCep()
        {
            var cep = "00000000"; // CEP inexistente
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var controller = new CepController();

            var result = await controller.ConsultaCep(cep);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.Equal("CEP não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public async Task ConsultaCep_ReturnsBadRequest_WithInvalidCep()
        {
            var cep = "000000"; // CEP inválido
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var controller = new CepController();

            var result = await controller.ConsultaCep(cep);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("Ocorreu um erro na solicitação: BadRequest Error - Verifique o CEP informado e tente novamente.", badRequestResult.Value);
        }
    }
}
