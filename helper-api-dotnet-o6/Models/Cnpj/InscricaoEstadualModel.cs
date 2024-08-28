    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text.Json.Serialization;
    namespace helper_api_dotnet_o6.Models.Cnpj
    {
    public class InscricaoEstadualModel
    {
    [JsonPropertyName("number")]
    public string numero { get; set; } 

    [JsonPropertyName("state")]
    public string estado { get; set; } 

    [JsonPropertyName("enabled")]
    public Boolean  ativa { get; set; } 

    [JsonPropertyName("statusDate")]
    public string dataStatus { get; set; } 

    [JsonPropertyName("status")]
    public Situacao situacao { get; set; } 

    [JsonPropertyName("type")]
    public TipoIe tipoIe { get; set; } 
    }
    }