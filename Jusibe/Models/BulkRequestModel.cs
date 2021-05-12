using Newtonsoft.Json;

namespace Jusibe.Models
{
    public class BulkRequestModel
    {
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
