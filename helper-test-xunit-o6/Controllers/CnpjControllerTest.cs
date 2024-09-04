using helper_api_dotnet_o6.Controllers;
using helper_api_dotnet_o6.Models.Cnpj;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace helper_test_xunit_o6.Controllers
{
    public class CnpjControllerTest
    {
        [Fact]
        public async Task ExecutaRotaCnpj_QuandoCnpjValido_EntaoRetornaDetalhesCnpj()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CnpjController>>();
            var controller = new CnpjController(loggerMock.Object);
            var cnpj = "05570714000159";            
            
            // Act
            var result = await controller.Get(cnpj);

            // Verifique o tipo do ActionResult
            var actionResult = Assert.IsType<ActionResult<Root>>(result);

            // Verifique se o resultado é OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            // Verifique se o valor retornado é do tipo Root
            var cnpjDetails = Assert.IsType<Root>(okResult.Value);

            // Assert

            Assert.NotNull(cnpjDetails);
        }
    }
}
