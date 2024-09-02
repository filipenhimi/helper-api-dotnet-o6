using helper_api_dotnet_o6.Models.Vehicle;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace helper_api_dotnet_o6.Helpers
{
    public class HttpRequestHelper
    {
        private string _endPoint;

        public HttpRequestHelper(string endPoint)
        {
            _endPoint= endPoint;
        }

        public async Task<T> Get<T>(string route)
        {
            HttpClient httpClient = new();
            var response = await httpClient.GetAsync($"{_endPoint}/{route}");
            string responseContent = await response.Content.ReadAsStringAsync();
            var deserializeResponse = JsonConvert.DeserializeObject<T>(responseContent);
            return deserializeResponse;
           

            
            
        }
    }
}
