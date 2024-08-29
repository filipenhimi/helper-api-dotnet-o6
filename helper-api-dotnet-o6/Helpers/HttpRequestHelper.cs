using Newtonsoft.Json;

namespace helper_api_dotnet_o6.Helpers
{
    public class HttpRequestHelper
    {
        private string _endPoint;
        private readonly HttpClient _httpClient;

        public HttpRequestHelper(string endPoint)
        {
            _endPoint = endPoint;
            _httpClient = new HttpClient();
        }

        public HttpRequestHelper(string endPoint, HttpClient httpClient)
        {
            _endPoint = endPoint;
            _httpClient = httpClient;
        }

        public async Task<T> Get<T>(string route)
        {
            var response = await _httpClient.GetAsync($"{_endPoint}/{route}");
            string responseContent = await response.Content.ReadAsStringAsync();
            var deserializeResponse = JsonConvert.DeserializeObject<T>(responseContent);
            return deserializeResponse;
        }
    }
}
