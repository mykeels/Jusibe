using Newtonsoft.Json;

namespace Jusibe.Models
{
    public class BulkResponseModel
    {
        [JsonProperty("bulk_message_id")]
        public string BulkMessageId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}