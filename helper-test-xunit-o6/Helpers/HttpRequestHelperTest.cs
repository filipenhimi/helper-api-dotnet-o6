using helper_api_dotnet_o6.Helpers;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace helper_test_xunit_o6.Helpers
{
    public class HttpRequestHelperTest
    {
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

        public class Country
        {
            public string Nome { get; set; }
            public string Moeda { get; set; }
        }
    }
}
