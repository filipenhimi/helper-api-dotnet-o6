using Newtonsoft.Json;

namespace helper_api_dotnet_o6.Models.Country
{
    public class Area
    {
        public string total { get; set; }
        public Unidade unidade { get; set; }
    }

    public class Capital
    {
        public string nome { get; set; }
    }

    public class Governo
    {
        public Capital capital { get; set; }
    }

    public class Id
    {
        public int M49 { get; set; }

        [JsonProperty("ISO-3166-1-ALPHA-2")]
        public string ISO31661ALPHA2 { get; set; }

        [JsonProperty("ISO-3166-1-ALPHA-3")]
        public string ISO31661ALPHA3 { get; set; }

        [JsonProperty("ISO-639-1")]
        public string ISO6391 { get; set; }

        [JsonProperty("ISO-639-2")]
        public string ISO6392 { get; set; }

        [JsonProperty("ISO-4217-ALPHA")]
        public string ISO4217ALPHA { get; set; }

        [JsonProperty("ISO-4217-NUMERICO")]
        public string ISO4217NUMERICO { get; set; }
    }

    public class Lingua
    {
        public Id id { get; set; }
        public string nome { get; set; }
    }

    public class Localizacao
    {
        public Regiao regiao { get; set; }

        [JsonProperty("sub-regiao")]
        public SubRegiao subregiao { get; set; }

        [JsonProperty("regiao-intermediaria")]
        public RegiaoIntermediaria regiaointermediaria { get; set; }
    }

    public class Nome
    {
        public string abreviado { get; set; }

        [JsonProperty("abreviado-EN")]
        public string abreviadoEN { get; set; }

        [JsonProperty("abreviado-ES")]
        public string abreviadoES { get; set; }
    }

    public class Regiao
    {
        public Id id { get; set; }
        public string nome { get; set; }
    }

    public class RegiaoIntermediaria
    {
        public Id id { get; set; }
        public string nome { get; set; }
    }

    public class Country
    {
        public Id id { get; set; }
        public Nome nome { get; set; }
        public Area area { get; set; }
        public Localizacao localizacao { get; set; }
        public List<Lingua> linguas { get; set; }
        public Governo governo { get; set; }

        [JsonProperty("unidades-monetarias")]
        public List<UnidadesMonetaria> unidadesmonetarias { get; set; }
        public string historico { get; set; }
    }

    public class SubRegiao
    {
        public Id id { get; set; }
        public string nome { get; set; }
    }

    public class Unidade
    {
        public string nome { get; set; }
        public string smbolo { get; set; }
        public int multiplicador { get; set; }
    }

    public class UnidadesMonetaria
    {
        public Id id { get; set; }
        public string nome { get; set; }
    }
}
