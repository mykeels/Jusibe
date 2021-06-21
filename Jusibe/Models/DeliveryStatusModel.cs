using Newtonsoft.Json;

namespace Jusibe.Models
{
    public class DeliveryStatusModel
    {
        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("date_sent")]
        public string DateSent { get; set; }

        [JsonProperty("date_delivered")]
        public string DateDelivered { get; set; }
    }
}
