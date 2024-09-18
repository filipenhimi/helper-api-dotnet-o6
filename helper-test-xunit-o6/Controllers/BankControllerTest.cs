using helper_api_dotnet_o6.Controllers;
using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Bank;
using Microsoft.Extensions.Logging;
using Moq;

namespace helper_test_xunit_o6.Controllers
{
    public class BankControllerTest
    {
        [Fact]
        public async Task ExecutaRotaBank_QuandoNomeExiste_EntaoRetornaListaDeBancos()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<BankController>>();
            var controller = new BankController(loggerMock.Object);
            var nome = "Santander";

            //Act
            var result = await controller.Get(nome);

            //Assert
            Assert.IsType<List<Bank>>(result);
        }
    }
}
