using System;
using Newtonsoft.Json;

namespace helper_api_dotnet_o6.Models.Feriados
{
    public class FeriadosModel
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
