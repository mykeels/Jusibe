using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jusibe.Models
{
    public class ResponseModel
    {
        [ JsonProperty("status") ]
        public string Status { get; set; }

        [ JsonProperty("message_id") ]
        public string MessageId { get; set; }

        [ JsonProperty("sms_credits_used") ]
        public int SmsCredits { get; set; }
    }
}
