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

        public NetworkCredential Credentials {
            get {
                var credential = new NetworkCredential();
                credential.UserName = this.PublicKey;
                credential.Password = this.AccessToken;
                return credential;
            }
        }

        public string ResolveURL(string url) {
            return this.RootUrl + url?.TrimStart('/');
        }
    }
}
