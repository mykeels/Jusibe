using Newtonsoft.Json;
using System;

namespace Jusibe.Models
{
    public class BulkStatusResponseModel
    {
        [JsonProperty("bulk_message_id")]
        public string BulkMessageId { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Processed { get; set; }

        [JsonProperty("total_numbers")]
        public int TotalNumbers { get; set; }

        [JsonProperty("total_unique_numbers")]
        public int TotalUniqueNumbers { get; set; }

        [JsonProperty("total_valid_numbers")]
        public int TotalValidNumbers { get; set; }

        [JsonProperty("total_invalid_numbers")]
        public int TotalInvalidNumbers { get; set; }
    }
}
