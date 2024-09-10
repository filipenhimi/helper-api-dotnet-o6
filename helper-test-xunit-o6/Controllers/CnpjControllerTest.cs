using helper_api_dotnet_o6.Controllers;
using helper_api_dotnet_o6.Models.CnpjModel;
using Microsoft.Extensions.Logging;
using Moq;

namespace helper_test_xunit_o6.Controllers
{
    public class CnpjControllerTest
    {
        [Fact]
        public async Task ExecutaRotaCnpj_QuandoCnpjValido_EntaoRetornaDadosDaEmpresa()
        {
            var loggerMock = new Mock<ILogger<CnpjController>>();
            var controller = new CnpjController(loggerMock.Object);
            var cnpj = "27865757000102";

            var result = await controller.Get(cnpj);

            Assert.IsType<CnpjModel>(result);
        }
    }
}
