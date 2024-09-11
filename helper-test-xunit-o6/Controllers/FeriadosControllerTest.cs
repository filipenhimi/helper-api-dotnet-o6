using helper_api_dotnet_o6.Controllers;
using helper_api_dotnet_o6.Models.Feriados;
using Microsoft.Extensions.Logging;
using Moq;
using helper_test_xunit_o6.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace helper_test_xunit_o6.Controllers
{
    // Classe de testes para o FeriadosController
    // Este teste verifica se o controlador retorna corretamente uma lista de feriados
    public class FeriadosControllerTest
    {
        // Teste para verificar se a rota "Feriados" com um ano válido
        // retorna uma lista de objetos FeriadosModel com sucesso
        [Fact]
        public async Task ExecutaRotaFeriados_QuandoAnoValido_EntaoRetornaListaDeFeriados()
        {
            // Arrange - Preparação dos dados e do cenário

            // Mockando o logger, pois ele é uma dependência do controlador
            var loggerMock = new Mock<ILogger<FeriadosController>>();

            // Instancia o controlador de feriados com o logger mockado
            var controller = new FeriadosController(loggerMock.Object);

            // Definindo o ano válido a ser passado para o controlador
            var ano = "2024";

            // Act - Execução da ação a ser testada
            var result = await controller.Get(ano);

            // Assert - Verificação dos resultados


            var okResult = Assert.IsType<OkObjectResult>(result); // Verifica se o resultado é OkObjectResult
            var feriados = Assert.IsType<List<FeriadosModel>>(okResult.Value); // Verifica se o valor retornado é uma lista de FeriadosModel
        }

    }
}
