using Newtonsoft.Json;

namespace helper_api_dotnet_o6.Models.Holiday
{
    public class Holiday
    {
        [JsonProperty("date")]
        public string Data { get; set; }

        [JsonProperty("name")]
        public string Nome { get; set; }

        [JsonProperty("type")]
        public string Tipo { get; set; }
    }
}
