using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace helper_api_dotnet_o6.Models.Exchange
{
    public class CurrencyExchange
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("codein")]
        public string CodeIn { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("high")]
        public string High { get; set; }

        [JsonProperty("low")]
        public string Low { get; set; }

        [JsonProperty("varBid")]
        public string VarBid { get; set; }

        [JsonProperty("pctChange")]
        public string PctChange { get; set; }

        [JsonProperty("bid")]
        public string Bid { get; set; }

        [JsonProperty("ask")]
        public string Ask { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("create_date")]
        public string CreateDate { get; set; }
    }

    public class ExchangeResponse
    {
        [JsonExtensionData]
        public IDictionary<string, JToken> Exchanges { get; set; }
    }
}
