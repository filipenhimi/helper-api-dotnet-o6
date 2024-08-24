  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Text.Json.Serialization;
  namespace helper_api_dotnet_o6.Models.Cnpj
  {
  public class SimplesNacional
  {
  [JsonPropertyName("optant")]
  public Boolean  optante { get; set; }

  [JsonPropertyName("since")]
  public string  dataOpcao { get; set; }
  }
  }