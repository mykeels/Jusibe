using System;
using Xunit;
using Jusibe;
using Jusibe.Models;
using System.Configuration;
using DotNetEnv;
using Newtonsoft.Json;

namespace Jusibe.Tests
{
    public class UrlTest
    {
        [Fact]
        public void Url_Is_Generated_Properly()
        {
            var config = new SMSConfig() {
                
            };

            Assert.Equal("https://jusibe.com/smsapi/send_sms", config.ResolveURL("send_sms"));
        }

        [Fact]
        public void Network_Credentials_Are_Generated_Properly()
        {
            var config = new SMSConfig() {
                AccessToken = System.Environment.GetEnvironmentVariable("Jusibe_Token"),
                PublicKey = System.Environment.GetEnvironmentVariable("Jusibe_Key")
            };

            Assert.NotNull(config.Credentials.UserName);
            Assert.NotNull(config.Credentials.Password);
        }
    }
}
