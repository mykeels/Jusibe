using Jusibe;
using Jusibe.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jusibe
{
    public class JusibeClient : IJusibeClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public JusibeClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;  
        }

        public async Task<ResponseModel> SendSms(RequestModel model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "send_sms");
            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var client = _clientFactory.CreateClient("jusibe");
            var response = await client.SendAsync(request);

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
            var request = new HttpRequestMessage(HttpMethod.Post, "get_credits");
            var client = _clientFactory.CreateClient("jusibe");
            var response = await client.SendAsync(request);

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
            var request = new HttpRequestMessage(HttpMethod.Post, $"delivery_status?message_id={messageId}");
            var client = _clientFactory.CreateClient("jusibe");
            var response = await client.SendAsync(request);

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
            var request = new HttpRequestMessage(HttpMethod.Post, "bulk/send_sms");
            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var client = _clientFactory.CreateClient("jusibe");
            var response = await client.SendAsync(request);

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
            var request = new HttpRequestMessage(HttpMethod.Post, $"bulk/status?bulk_message_id={bulkMessageId}");
            var client = _clientFactory.CreateClient("jusibe");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
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
