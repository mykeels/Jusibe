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
        protected void LoadEnvironment() {
            string filename = "../../../.env";
            
            if (System.IO.File.Exists(filename)) {
                DotNetEnv.Env.Load(filename);
            }
            else {
                ConsoleColor foreground = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Warning: No Env File Found");
                Console.ForegroundColor = foreground;
            }
        }

        [Fact]
        public void Url_Is_Generated_Properly()
        {
            this.LoadEnvironment();

            var config = new SMSConfig() {
                
            };

            Assert.Equal("https://jusibe.com/smsapi/send_sms", config.ResolveURL("send_sms"));
        }

        [Fact]
        public void Network_Credentials_Are_Generated_Properly()
        {
            this.LoadEnvironment();

            var config = new SMSConfig() {
                AccessToken = System.Environment.GetEnvironmentVariable("Jusibe_Token"),
                PublicKey = System.Environment.GetEnvironmentVariable("Jusibe_Key")
            };

            Assert.NotNull(config.Credentials.UserName);
            Assert.NotNull(config.Credentials.Password);
        }
    }
}
