using System;
using Xunit;
using Jusibe;
using Jusibe.Models;
using System.Configuration;
using DotNetEnv;
using Newtonsoft.Json;

namespace Jusibe.Tests
{
    public class SmsTest
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
        public void Sms_Gets_Sent()
        {
            this.LoadEnvironment();

            JusibeClient client = new JusibeClient(new SMSConfig() {
                AccessToken = System.Environment.GetEnvironmentVariable("Jusibe_Token"),
                PublicKey = System.Environment.GetEnvironmentVariable("Jusibe_Key")
            });

            var result = client.Send(new RequestModel() {
                From = "Mykeels",
                To = System.Environment.GetEnvironmentVariable("Phone"),
                Message = "Hello World"
            }).Result;

            Assert.NotNull(result.MessageId);
            Assert.Equal("Sent", result.Status);
            Assert.Equal(1, result.SmsCredits);

            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        [Fact]
        public void Sms_Get_Credits()
        {
            this.LoadEnvironment();

            DotNetEnv.Env.Load("../../../.env");
            
            JusibeClient client = new JusibeClient(new SMSConfig() {
                AccessToken = System.Environment.GetEnvironmentVariable("Jusibe_Token"),
                PublicKey = System.Environment.GetEnvironmentVariable("Jusibe_Key")
            });

            var result = client.GetCredits().Result;

            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        [Fact]
        public void Sms_Get_Delivery_Status()
        {
            DotNetEnv.Env.Load("../../../.env");
            
            JusibeClient client = new JusibeClient(new SMSConfig() {
                AccessToken = System.Environment.GetEnvironmentVariable("Jusibe_Token"),
                PublicKey = System.Environment.GetEnvironmentVariable("Jusibe_Key")
            });

            var result = client.GetDeliveryStatus(Environment.GetEnvironmentVariable("MessageId")).Result;

            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
