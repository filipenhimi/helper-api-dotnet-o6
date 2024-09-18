  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Text.Json.Serialization;
  namespace helper_api_dotnet_o6.Models.Cnpj
  {
  public class Socios
  {
  [JsonPropertyName("since")]
  public string  dataAdmissao { get; set; } 

  [JsonPropertyName("person")]
  public Pessoa  pessoa { get; set; } 

  [JsonPropertyName("role")]
  public Cargo  cargo { get; set; } 
  }
  }