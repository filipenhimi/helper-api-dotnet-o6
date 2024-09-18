using Newtonsoft.Json;

namespace helper_api_dotnet_o6_investimento.Infra
{
    public class ApiBase
    {
        public string BaseUri;

        public async Task<T> GetAsync<T>(string action)
        {
            try
            {
                var url = $"{BaseUri}{action}";
                using (HttpClient httpClient = new())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseData);
                }
            }
            catch (HttpRequestException e)
            {
                // Trate exceções específicas de requisições HTTP
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                // Trate outras exceções
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }
    }
}