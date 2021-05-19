using Jusibe.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jusibe
{
    public class JusibeClient : IJusibeClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JusibeClientOptions _jusibeClientOptions;
        private readonly HttpClient _httpClient;

        public JusibeClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _httpClient = _clientFactory.CreateClient("jusibe");
        }

        public JusibeClient(JusibeClientOptions options)
        {
            _jusibeClientOptions = options;
            _httpClient = new HttpClient();
        }

        public async Task<ResponseModel> SendSms(RequestModel model)
        {
            HttpRequestMessage request;
            if (_clientFactory == null)
            {
                request = new HttpRequestMessage(HttpMethod.Post, $"{_jusibeClientOptions.BaseAddress}/send_sms");
                request.Headers.Add("ContentType", "application/json");
                request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_jusibeClientOptions.Key}:{_jusibeClientOptions.Token}"))}");
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Post, "send_sms");
            }

            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseModel = JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                return responseModel;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<CreditModel> GetCredits()
        {
            HttpRequestMessage request;
            if (_clientFactory == null)
            {
                request = new HttpRequestMessage(HttpMethod.Post, $"{_jusibeClientOptions.BaseAddress}get_credits");
                request.Headers.Add("ContentType", "application/json");
                request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_jusibeClientOptions.Key}:{_jusibeClientOptions.Token}"))}");
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Post, "get_credits");
            }

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseModel = JsonConvert.DeserializeObject<CreditModel>(await response.Content.ReadAsStringAsync());
                return responseModel;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<DeliveryStatusModel> GetDeliveryStatus(string messageId)
        {
            HttpRequestMessage request;
            if (_clientFactory == null)
            {
                request = new HttpRequestMessage(HttpMethod.Post, $"{_jusibeClientOptions.BaseAddress}/delivery_status?message_id={messageId}");
                request.Headers.Add("ContentType", "application/json");
                request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_jusibeClientOptions.Key}:{_jusibeClientOptions.Token}"))}");
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Post, $"delivery_status?message_id={messageId}");
            }

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseModel = JsonConvert.DeserializeObject<DeliveryStatusModel>(await response.Content.ReadAsStringAsync());
                return responseModel;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<BulkResponseModel> SendBulkSms(BulkRequestModel model)
        {
            HttpRequestMessage request;
            if (_clientFactory == null)
            {
                request = new HttpRequestMessage(HttpMethod.Post, $"{_jusibeClientOptions.BaseAddress}/bulk/send_sms");
                request.Headers.Add("ContentType", "application/json");
                request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_jusibeClientOptions.Key}:{_jusibeClientOptions.Token}"))}");
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Post, "bulk/send_sms");
            }

            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseModel = JsonConvert.DeserializeObject<BulkResponseModel>(await response.Content.ReadAsStringAsync());
                return responseModel;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<BulkStatusResponseModel> GetBulkSmsStatus(string bulkMessageId)
        {
            HttpRequestMessage request;
            if (_clientFactory == null)
            {
                request = new HttpRequestMessage(HttpMethod.Post, $"{_jusibeClientOptions.BaseAddress}bulk/status?bulk_message_id={bulkMessageId}");
                request.Headers.Add("ContentType", "application/json");
                request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_jusibeClientOptions.Key}:{_jusibeClientOptions.Token}"))}");
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Post, $"bulk/status?bulk_message_id={bulkMessageId}");
            }

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<BulkStatusResponseModel>(await response.Content.ReadAsStringAsync());
                return responseModel;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
