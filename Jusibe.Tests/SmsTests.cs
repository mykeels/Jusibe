using Bogus;
using Jusibe.Models;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Jusibe.Tests
{
    public class SmsTests
    {
        [Fact]
        public async Task Sms_Gets_Sent()
        {
            var request = new Faker<RequestModel>().Generate();
            var response = new Faker<ResponseModel>()
                .RuleFor(r => r.MessageId, x => x.Random.AlphaNumeric(6))
                .RuleFor(r => r.Status, _ => "Sent")
                .RuleFor(r => r.SmsCredits, _ => 1)
                .Generate();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(response))
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://jusibe.com/smsapi/"),
            };
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var jusibeClient = new JusibeClient(mockFactory.Object);
            var result = await jusibeClient.SendSms(request);

            Assert.NotNull(result.MessageId);
            Assert.Equal("Sent", result.Status);
            Assert.Equal(1, result.SmsCredits);
        }

        [Fact]
        public async Task Can_Get_Sms_Credits()
        {
            var response = new Faker<CreditModel>()
                .RuleFor(r => r.SmsCredits, _ => 1)
                .Generate();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(response))
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://jusibe.com/smsapi/"),
            };
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var jusibeClient = new JusibeClient(mockFactory.Object);
            var result = await jusibeClient.GetCredits();

            Assert.NotEqual(0, result.SmsCredits);
        }

        [Fact]
        public async Task Can_Get_Sms_Delivery_Status()
        {
            var messageId = new Faker().Random.AlphaNumeric(6);
            var response = new Faker<DeliveryStatusModel>()
                .RuleFor(r => r.MessageId, _ => messageId)
                .RuleFor(r => r.Status, _ => "Completed")
                .Generate();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(response))
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://jusibe.com/smsapi/"),
            };
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var jusibeClient = new JusibeClient(mockFactory.Object);
            var result = await jusibeClient.GetDeliveryStatus(messageId);

            Assert.Equal(messageId, result.MessageId);
            Assert.Equal("Completed", response.Status);
        }
    
        [Fact]
        public async Task Bulk_Sms_Gets_Sent()
        {
            var request = new Faker<BulkRequestModel>().Generate();
            var response = new Faker<BulkResponseModel>()
                .RuleFor(r => r.BulkMessageId, x => x.Random.AlphaNumeric(6))
                .RuleFor(r => r.Status, _ => "Sent")
                .Generate();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(response))
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://jusibe.com/smsapi/"),
            };
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var jusibeClient = new JusibeClient(mockFactory.Object);
            var result = await jusibeClient.SendBulkSms(request);

            Assert.NotNull(result.BulkMessageId);
            Assert.Equal("Sent", result.Status);
        } 
        
        [Fact]
        public async Task Can_Get_Bulk_Sms_Delivery_Status()
        {
            var bulkMessageId = new Faker().Random.AlphaNumeric(6);
            var response = new Faker<BulkStatusResponseModel>()
                .RuleFor(r => r.BulkMessageId, _ => bulkMessageId)
                .RuleFor(r => r.Status, _ => "Completed")
                .RuleFor(r => r.TotalInvalidNumbers, _ => 1)
                .Generate();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(response))
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://jusibe.com/smsapi/"),
            };
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var jusibeClient = new JusibeClient(mockFactory.Object);
            var result = await jusibeClient.GetBulkSmsStatus(bulkMessageId);

            Assert.Equal(bulkMessageId, result.BulkMessageId);
            Assert.Equal("Completed", response.Status);
            Assert.Equal(1, result.TotalInvalidNumbers);
        }
    }
}
