using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Extensions;
using System.Net;

namespace Jusibe
{
    public class Client
    {
        public string publicKey { get; set; }
        public string accessToken { get; set; }
        

        public Client(string key = null, string token = null)
        {
            if (!String.IsNullOrEmpty(key)) this.publicKey = key;
            else this.publicKey = ConfigurationManager.AppSettings["Jusibe_Public_Key"];
            if (!String.IsNullOrEmpty(token)) this.accessToken = token;
            else this.accessToken = ConfigurationManager.AppSettings["Jusibe_Access_Token"];
        }

        private NetworkCredential _getCredentials()
        {
            var credentials = new NetworkCredential();
            credentials.UserName = this.publicKey;
            credentials.Password = this.accessToken;
            return credentials;
        }

        public Promise<Models.SMS.Response> SendSms(Models.SMS.Request request)
        {
            var credentials = _getCredentials();
            return Api.PostAsync<Models.SMS.Response>(Models.SMS.GetEndpointUrl(), request.ToJson(),
                "application/json", null, true, credentials).Error((Exception ex) =>
                {
                    throw ex;
                });
        }

        public Promise<string> SendSmsString(Models.SMS.Request request)
        {
            var credentials = _getCredentials();
            return Api.PostAsync(Models.SMS.GetEndpointUrl(), request.ToJson(),
                "application/json", null, true, credentials).Error((Exception ex) =>
                {
                    throw ex;
                });
        }

        public Promise<Models.Credit> GetCredits()
        {
            var credentials = _getCredentials();
            return Api.GetAsync<Models.Credit>(Models.Credit.GetEndpointUrl(), null, credentials).Error((Exception ex) =>
            {
                throw ex;
            });
        }

        public Promise<Models.SMS.DeliveryStatus> CheckDelivery(string message_id)
        {
            var credentials = _getCredentials();
            return Api.GetAsync<Models.SMS.DeliveryStatus>(Models.SMS.DeliveryStatus.GetEndpointUrl(message_id), 
                null, credentials).Error((Exception ex) =>
                {
                    throw ex;
                });
        }
    }
}
