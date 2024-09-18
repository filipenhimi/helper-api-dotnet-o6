using helper_api_dotnet_o6.Controllers;
using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Country;
using Microsoft.Extensions.Logging;
using Moq;

namespace helper_test_xunit_o6.Controllers
{
    // Classe de testes para o CountryController
    // Este teste verifica se a rota que busca dados de um país, dada uma sigla válida,
    // retorna corretamente uma lista de objetos Country
    public class CountryControllerTest
    {
        [Fact]
        public async Task ExecutaRotaCountry_QuandoSiglaValida_EntaoRetornaListaDadosDoPais()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<CountryController>>();
            var controller = new CountryController(loggerMock.Object);
            var siglas = "BR";

            //Act
            var result = await controller.Get(siglas);

            //Assert
            Assert.IsType<List<Country>>(result);
        }
    }
}
