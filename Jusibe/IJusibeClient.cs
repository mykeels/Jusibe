using Jusibe.Models;
using System.Threading.Tasks;

namespace Jusibe
{
    public interface IJusibeClient
    {
        Task<CreditModel> GetCredits();
        Task<DeliveryStatusModel> GetDeliveryStatus(string messageId);
        Task<ResponseModel> SendSms(RequestModel model);
        Task<BulkResponseModel> SendBulkSms(BulkRequestModel model);
        Task<BulkStatusResponseModel> GetBulkSmsStatus(string bulkMessageId);
    }
}
