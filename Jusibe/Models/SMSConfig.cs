using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;

namespace Jusibe.Models
{
    public class SMSConfig
    {
        public string RootUrl { get; set; } = "https://jusibe.com/smsapi/";
        public string PublicKey { get; set; }
        public string AccessToken { get; set; }

        public string ResolveURL(string url) {
            return this.RootUrl + url?.TrimStart('/');
        }
    }
}
