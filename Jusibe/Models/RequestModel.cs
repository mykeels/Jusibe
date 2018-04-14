using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jusibe.Models
{
    public class RequestModel
    {
        [ JsonProperty("to") ]
        public string To { get; set; }

        [ JsonProperty("from") ]
        public string From { get; set; }

        [ JsonProperty("message") ]
        public string Message { get; set; }

        public string AsQuery() {
            return "to=" + WebUtility.UrlEncode(this.To) + 
                    "&from=" + WebUtility.UrlEncode(this.From) + 
                    "&message=" + WebUtility.UrlEncode(this.Message);
        }
    }
}
