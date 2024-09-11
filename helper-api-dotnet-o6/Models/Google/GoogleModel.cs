using Newtonsoft.Json;

namespace helper_api_dotnet_o6.Models.Google
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AnswerBox
    {
        public string type { get; set; }
        public string syllables { get; set; }
        public string pronunciation_audio { get; set; }
        public string phonetic { get; set; }
        public string word_type { get; set; }
        public List<string> definitions { get; set; }
        public List<string> examples { get; set; }
        public List<string> extras { get; set; }
    }

    public class DmcaMessages
    {
        public string title { get; set; }
        public List<Message> messages { get; set; }
    }

    public class HighlightedWord
    {
        public string link { get; set; }
        public string text { get; set; }
    }

    public class Inline
    {
        public string title { get; set; }
        public string link { get; set; }
    }

    public class InlineImage
    {
        public string link { get; set; }
        public string source { get; set; }
        public string thumbnail { get; set; }
        public string original { get; set; }
        public string title { get; set; }
        public string source_name { get; set; }
    }

    public class InlineVideo
    {
        public int position { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string thumbnail { get; set; }
        public string channel { get; set; }
        public string duration { get; set; }
        public string platform { get; set; }
        public string date { get; set; }
        public List<KeyMoment> key_moments { get; set; }
    }

    public class KeyMoment
    {
        public string time { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string thumbnail { get; set; }
    }

    public class Message
    {
        public string content { get; set; }
        public List<HighlightedWord> highlighted_words { get; set; }
    }

    public class OrganicResult
    {
        public int position { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string redirect_link { get; set; }
        public string displayed_link { get; set; }
        public string favicon { get; set; }
        public string snippet { get; set; }
        public string source { get; set; }
        public List<string> snippet_highlighted_words { get; set; }
        public Sitelinks sitelinks { get; set; }
    }

    public class OtherPages
    {
        [JsonProperty("2")]
        public string _2 { get; set; }

        [JsonProperty("3")]
        public string _3 { get; set; }

        [JsonProperty("4")]
        public string _4 { get; set; }

        [JsonProperty("5")]
        public string _5 { get; set; }

        [JsonProperty("6")]
        public string _6 { get; set; }

        [JsonProperty("7")]
        public string _7 { get; set; }

        [JsonProperty("8")]
        public string _8 { get; set; }

        [JsonProperty("9")]
        public string _9 { get; set; }

        [JsonProperty("10")]
        public string _10 { get; set; }
    }

    public class Pagination
    {
        public int current { get; set; }
        public string next { get; set; }
        public OtherPages other_pages { get; set; }
    }

    public class RelatedSearch
    {
        public int block_position { get; set; }
        public string query { get; set; }
        public string link { get; set; }
        public string serpapi_link { get; set; }
    }

    public class Google
    {
        public SearchMetadata search_metadata { get; set; }
        public SearchParameters search_parameters { get; set; }
        public SearchInformation search_information { get; set; }
        public List<InlineImage> inline_images { get; set; }
        public List<InlineVideo> inline_videos { get; set; }
        public AnswerBox answer_box { get; set; }
        public List<OrganicResult> organic_results { get; set; }
        public List<RelatedSearch> related_searches { get; set; }
        public DmcaMessages dmca_messages { get; set; }
        public Pagination pagination { get; set; }
        public SerpapiPagination serpapi_pagination { get; set; }
    }

    public class SearchInformation
    {
        public string query_displayed { get; set; }
        public long total_results { get; set; }
        public double time_taken_displayed { get; set; }
        public string organic_results_state { get; set; }
    }

    public class SearchMetadata
    {
        public string id { get; set; }
        public string status { get; set; }
        public string json_endpoint { get; set; }
        public string created_at { get; set; }
        public string processed_at { get; set; }
        public string google_url { get; set; }
        public string raw_html_file { get; set; }
        public double total_time_taken { get; set; }
    }

    public class SearchParameters
    {
        public string engine { get; set; }
        public string q { get; set; }
        public string google_domain { get; set; }
        public string device { get; set; }
    }

    public class SerpapiPagination
    {
        public int current { get; set; }
        public string next_link { get; set; }
        public string next { get; set; }
        public OtherPages other_pages { get; set; }
    }

    public class Sitelinks
    {
        public List<Inline> inline { get; set; }
    }


}
