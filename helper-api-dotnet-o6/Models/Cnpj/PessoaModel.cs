  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Text.Json.Serialization;
  namespace helper_api_dotnet_o6.Models.Cnpj
  {
  public class Pessoa
  {
  [JsonPropertyName("id")]
  public string  id { get; set; }  

  [JsonPropertyName("type")]
  public string  tipo { get; set; } 

  [JsonPropertyName("name")]
  public string  nome { get; set; } 

  [JsonPropertyName("taxId")]
  public string  CPF { get; set; } 

  [JsonPropertyName("age")]
  public string  idade { get; set; } 
  }
  }