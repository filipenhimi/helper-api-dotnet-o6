  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Text.Json.Serialization;
  namespace helper_api_dotnet_o6.Models.Cnpj
  {
  public class Porte
  {
  [JsonPropertyName("id")]
  public int  id { get; set; } 

  [JsonPropertyName("acronym")]
  public string  acronimo { get; set; }

  [JsonPropertyName("text")]
  public string  descricao { get; set; }
  }
  }