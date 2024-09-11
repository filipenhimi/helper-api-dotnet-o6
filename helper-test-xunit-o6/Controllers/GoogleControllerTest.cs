using helper_api_dotnet_o6.Controllers;
using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Google;
using Microsoft.Extensions.Logging;
using Moq;

namespace helper_test_xunit_o6.Controllers
{
    public class GoogleControllerTest
    {
        [Fact]
        public async Task ExecutaRotaGoogle_BuscaTermo_EntaoRetornaListaResultados()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<GoogleController>>();
            var controller = new GoogleController(loggerMock.Object);
            var termo = "test";

            //Act
            var result = await controller.Get(termo);

            //Assert
            Assert.IsType<List<OrganicResult>>(result);
        }
    }
}
