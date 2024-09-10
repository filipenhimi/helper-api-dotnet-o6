using Newtonsoft.Json;

namespace helper_api_dotnet_o6.Models.Cnpj
{
    public class AtividadePrincipal
    {
        public string text { get; set; }
        public string code { get; set; }
    }

    public class AtividadeSecundaria
    {
        public string text { get; set; }
        public string code { get; set; }
    }

    public class Qsa
    {
        public string nome { get; set; }
        public string qual { get; set; }
    }

    public class CnpjModel
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("razao_social")]
        public string RazaoSocial { get; set; }

        [JsonProperty("nome_fantasia")]
        public string NomeFantasia { get; set; }

        [JsonProperty("data_abertura")]
        public string DataAbertura { get; set; }

        [JsonProperty("atividade_principal")]
        public List<AtividadePrincipal> AtividadePrincipal { get; set; }

        [JsonProperty("atividades_secundarias")]
        public List<AtividadeSecundaria> AtividadesSecundarias { get; set; }

        [JsonProperty("natureza_juridica")]
        public string NaturezaJuridica { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("municipio")]
        public string Municipio { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("qsa")]
        public List<Qsa> Qsa { get; set; }

        [JsonProperty("capital_social")]
        public string CapitalSocial { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
