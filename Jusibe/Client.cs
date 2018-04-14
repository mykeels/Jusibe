using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using Jusibe.Models;

namespace Jusibe
{
    public class Client
    {
        private SMSConfig config;

        public Client(SMSConfig config) {
            this.config = config;
        }

        public Client(string key, string token) {
            this.config = new SMSConfig() {
                AccessToken = token,
                PublicKey = key
            };
        }

        
    }
}
