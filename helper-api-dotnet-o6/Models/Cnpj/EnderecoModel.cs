  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Text.Json.Serialization;
  namespace helper_api_dotnet_o6.Models.Cnpj
  {
  public class Endereco
  {
  [JsonPropertyName("municipality")]
  public int id { get; set; }  

  [JsonPropertyName("zip")]
  public string cep { get; set; }  

  [JsonPropertyName("street")]
  public string rua { get; set; }  

  [JsonPropertyName("number")]
  public string numero { get; set; }  

  [JsonPropertyName("district")]
  public string bairro { get; set; }  

  [JsonPropertyName("city")]
  public string cidade { get; set; }  

  [JsonPropertyName("state")]
  public string estado { get; set; } 

  [JsonPropertyName("detais")]
  public string complemento { get; set; } 

  [JsonPropertyName("country")]
  public Pais pais { get; set; } 
  }
  }