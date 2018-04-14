using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jusibe.Models
{
    public class CreditModel
    {
        [ JsonProperty("sms_credits") ]
        public int SmsCredits { get; set; }
    }
}
