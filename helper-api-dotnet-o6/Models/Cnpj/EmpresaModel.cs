   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading.Tasks;
   using System.Text.Json.Serialization;
   namespace helper_api_dotnet_o6.Models.Cnpj
   {
   public class Empresa
   {
   [JsonPropertyName("members")]
   public Socios[]  socios { get; set; } 

   [JsonPropertyName("id")]
   public int  id { get; set; } 

   [JsonPropertyName("name")]
   public string  nome { get; set; } 

   [JsonPropertyName("equity")]
   public double  capitaSocial { get; set; } 

   [JsonPropertyName("nature")]
   public NaturezaJuridica  naturezaJuridicaocial { get; set;}

   [JsonPropertyName("size")]
   public Porte  porte { get; set;} 

   [JsonPropertyName("simples")]
   public SimplesNacional  simplesNacional { get; set;} 

   [JsonPropertyName("simei")]
   public Simei  simei { get; set;}
   }
   }