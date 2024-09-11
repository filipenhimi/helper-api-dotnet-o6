using helper_api_dotnet_o6.Controllers;
using helper_api_dotnet_o6.Models.Holiday;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace helper_test_xunit_o6.Controllers;
public class HolidayControllerTests
{
    private readonly Mock<ILogger<HolidayController>> _loggerMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;

    public HolidayControllerTests()
    {
        _loggerMock = new Mock<ILogger<HolidayController>>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
    }

    [Fact]
    public async Task ExecutaRotaFeriados_ListaFeriadosValidos_EntaoRetornaListagemFeriados()
    {
        var holidays = new List<Holiday>
        {
            new() { Data = "2024-01-01", Nome = "Ano Novo" },
            new() { Data = "2024-04-21", Nome = "Tiradentes" }
        };

        var responseContent = new StringContent(JsonConvert.SerializeObject(holidays));
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = responseContent };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var controller = new HolidayController(_loggerMock.Object, _httpClient);

        var result = await controller.Get(2024) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

        var resultValue = Assert.IsType<List<Holiday>>(result.Value);
        Assert.Equal(2, resultValue.Count);
    }

    [Fact]
    public async Task ExecutaRotaFeriados_AnoInvalido_EntaoRetornaFalhaNaRequisicao()
    {
        var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var controller = new HolidayController(_loggerMock.Object, _httpClient);

        var result = await controller.Get(1234);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task ExecutaRotaFeriados_ErroNaApiExterna_EntaoRetornaErroDesconhecido()
    {
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException());

        var controller = new HolidayController(_loggerMock.Object, _httpClient);

        var result = await controller.Get(2024);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
    }
}