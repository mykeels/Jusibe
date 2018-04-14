using System;
using Xunit;
using Jusibe;
using Jusibe.Models;
using System.Configuration;
using DotEnv.;

namespace Jusibe.Tests
{
    public class SmsTest
    {
        [Fact]
        public void Test1()
        {
            JusibeClient client = new JusibeClient(new SMSConfig() {
                AccessToken = 
            })
        }
    }
}
