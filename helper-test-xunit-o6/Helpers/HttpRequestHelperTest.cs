using helper_api_dotnet_o6.Helpers;
using helper_api_dotnet_o6.Models.Cnpj;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static helper_test_xunit_o6.Helpers.HttpRequestHelperTest;

namespace helper_test_xunit_o6.Helpers
{
    public class HttpRequestHelperTest
    {
        // Teste para verificar se uma chamada HTTP para uma rota válida retorna
        // corretamente um objeto JSON deserializado do tipo Country
        [Fact]
        public async Task RealizaChamadaHttp_QuandoRotaExistente_EntaoRetornaJSONDeserializado()
        {
            // Arrange
            var endPoint = "https://endpoint/api/v1";
            var route = "Country";

            var expectedResponse = new Country()
            {
                Nome = "Brasil",
                Moeda = "Real"
            };

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            
            var httpRequestHelper = new HttpRequestHelper(endPoint, httpClient);

            // Act
            var result = await httpRequestHelper.Get<Country>(route);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Brasil", result.Nome);
            Assert.Equal("Real", result.Moeda);
        }

        // Teste para verificar se a rota "Feriados" retorna corretamente uma lista de feriados
        [Fact]
        public async Task RealizaChamadaHttp_QuandoAnoValido_EntaoRetornaListaDeFeriados()
        {
            // Arrange - Preparação dos dados e do cenário
            var endPoint = "https://endpoint/api/v1";
            var route = "Feriados";    // Rota sendo testada

            // Lista de feriados esperados como resposta da chamada
            var expectedResponse = new List<Feriado>
            {
                new Feriado { Data = "2024-01-01T00:00:00", Nome = "Confraternização mundial"},
                new Feriado { Data = "2024-02-13T00:00:00", Nome = "Carnaval" },
                new Feriado { Data = "2024-03-29T00:00:00", Nome = "Sexta-feira Santa" },
                new Feriado { Data = "2024-03-31T00:00:00", Nome = "Páscoa" },
                new Feriado { Data = "2024-04-21T00:00:00", Nome = "Tiradentes" },
                new Feriado { Data = "2024-05-01T00:00:00", Nome = "Dia do trabalho" },
                new Feriado { Data = "2024-05-30T00:00:00", Nome = "Corpus Christi" },
                new Feriado { Data = "2024-09-07T00:00:00", Nome = "Independência do Brasil" },
                new Feriado { Data = "2024-10-12T00:00:00", Nome = "Nossa Senhora Aparecida" },
                new Feriado { Data = "2024-11-02T00:00:00", Nome = "Finados" },
                new Feriado { Data = "2024-11-15T00:00:00", Nome = "Proclamação da República" },
                new Feriado { Data = "2024-11-20T00:00:00", Nome = "Dia da consciência negra"},
                new Feriado { Data = "2024-12-25T00:00:00", Nome = "Natal"}
            };

            // Mockando o manipulador de mensagens HTTP para simular uma resposta da API
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectedResponse), Encoding.UTF8, "application/json")
                });

            // Criando um cliente HTTP com o manipulador simulado
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var httpRequestHelper = new HttpRequestHelper(endPoint, httpClient);

            // Act - Executando a ação
            var result = await httpRequestHelper.Get<List<Feriado>>(route);

            // Assert - Verificando se o resultado é o esperado
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Count, result.Count); // Verifica se o número de feriados coincide
            for (int i = 0; i < expectedResponse.Count; i++)
            {
                Assert.Equal(expectedResponse[i].Data, result[i].Data); // Verifica a Data de cada feriado
                Assert.Equal(expectedResponse[i].Nome, result[i].Nome); // Verifica o Nome de cada feriado
            }
        }

        // Teste para verificar se uma chamada HTTP para a rota "Cnpj" retorna corretamente
        // os detalhes de um CNPJ
        [Fact]
        public async Task RealizaChamadaHttp_QuandoRotaCnpjExistente_EntaoRetornaDetalhesCnpj()
        {
            // Arrange - Preparação dos dados e do cenário
            var endPoint = "https://endpoint/api/v1";
            var route = "Cnpj";

            // Objeto esperado como resposta da chamada
            var expectedResponse = new Root
            {
                cnpj = "05570714000159",
                razao_social = "KABUM S.A."
            };

            // Mockando o manipulador de mensagens HTTP para simular uma resposta da API
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectedResponse), Encoding.UTF8, "application/json")
                });

            // Criando um cliente HTTP com o manipulador simulado
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var httpRequestHelper = new HttpRequestHelper(endPoint, httpClient);

            // Act - Executando a ação
            var result = await httpRequestHelper.Get<Root>(route);

            // Assert - Verificando se o resultado é o esperado
            Assert.NotNull(result);
            Assert.Equal("05570714000159", result.cnpj);
            Assert.Equal("KABUM S.A.", result.razao_social);
        }

        // Classe auxiliar para deserialização da resposta da rota "Country"
        public class Country
        {
            public string Nome { get; set; }
            public string Moeda { get; set; }
        }

        // Classe auxiliar Feriado usada para a deserialização
        public class Feriado
        {
            public string Data { get; set; }
            public string Nome { get; set; }
        }

        // Classe auxiliar Cnpj usada para a deserialização
        public class Cnpj
        {
            public string CnpjNumero { get; set; }
            public string NomeEmpresarial { get; set; }
        }

    }
}
