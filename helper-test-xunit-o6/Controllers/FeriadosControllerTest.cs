using helper_api_dotnet_o6.Controllers;
using helper_api_dotnet_o6.Models.Feriados;
using Microsoft.Extensions.Logging;
using Moq;
using helper_test_xunit_o6.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace helper_test_xunit_o6.Controllers
{
    public class FeriadosControllerTest
    {
        [Fact]
        public async Task ExecutaRotaFeriados_QuandoAnoValido_EntaoRetornaListaDeFeriados()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<FeriadosController>>();
            var controller = new FeriadosController(loggerMock.Object);
            var ano = "2024";

            // Act
            var result = await controller.Get(ano);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verifica se o resultado é OkObjectResult
            var feriados = Assert.IsType<List<FeriadosModel>>(okResult.Value); // Verifica se o valor retornado é uma lista de FeriadosModel
        }

    }
}
