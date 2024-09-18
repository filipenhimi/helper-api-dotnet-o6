    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text.Json.Serialization;
    namespace helper_api_dotnet_o6.Models.Cnpj
    {
    public class Pais
    {
    [JsonPropertyName("id")]
    public int id { get; set; }  

    [JsonPropertyName("name")]
    public string nome { get; set; }  
    }
    }