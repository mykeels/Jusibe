using System;
using System.Linq;
using System.Net;
using System.Text;
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
                return new NetworkCredential(this.PublicKey, this.AccessToken);
            }
        }

        public string AuthorizationHeader {
            get {
                return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(this.PublicKey + ":" + this.AccessToken));
            }
        }

        public string ResolveURL(string url) {
            return this.RootUrl + url?.TrimStart('/');
        }
    }
}
