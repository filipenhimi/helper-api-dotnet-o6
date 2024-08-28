    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using helper_api_dotnet_o6.Models.Cnpj;
    using System.Text.Json.Serialization;
    namespace helper_api_dotnet_o6.Models
    {
    public class CnpjResponse
    {
    [JsonPropertyName("company")]
    public Empresa  empresa { get; set; }  

    [JsonPropertyName("taxId")]
    public string cnpj { get; set; }  

    [JsonPropertyName("updated")]
    public string dataAtualizacao { get; set; }  

    [JsonPropertyName("alias")]
    public string nomeFantasia { get; set; }  

    [JsonPropertyName("founded")]
    public string dataFundacao { get; set; }  


    [JsonPropertyName("head")]
    public Boolean  head { get; set; } 

    [JsonPropertyName("statusDate")]
    public string dataStatus { get; set; }  

    [JsonPropertyName("status")]
    public Situacao situacao { get; set; }  

    [JsonPropertyName("address")]
    public Endereco  endereco { get; set; } 

    [JsonPropertyName("phones")]
    public Telefone[] telefone  { get; set; } 

    [JsonPropertyName("emails")]
    public Email[] email { get; set; } 

    [JsonPropertyName("mainActivity")]
    public AtividadePrincipal atividadePrincipal { get; set; } 

    [JsonPropertyName("sideActivities")]
    public AtividadeSecundaria[] atividadeSecundaria { get; set; } 

    [JsonPropertyName("registrations")]
    public InscricaoEstadualModel[] inscricaoEstadual { get; set; } 

    [JsonPropertyName("suframa")]
    public Suframa[] suframa { get; set; } 
    }
    }