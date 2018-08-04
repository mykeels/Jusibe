using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Jusibe.Models;
using Jusibe.Common;
using Newtonsoft.Json;

namespace Jusibe
{
    public class JusibeClient
    {
        private SMSConfig config;

        public JusibeClient(SMSConfig config) {
            this.config = config;
        }

        public JusibeClient(string key, string token) {
            this.config = new SMSConfig() {
                AccessToken = token,
                PublicKey = key
            };
        }

        public async Task<ResponseModel> Send(RequestModel model) {
            var request = (HttpWebRequest)WebRequest.Create(this.config.ResolveURL("send_sms"));
            request.Method = Constants.POST;
            request.ContentType = Constants.X_WWW_FORM_URL_ENCODED;
            request.Credentials = this.config.Credentials;
            request.Headers.Add("Authorization", this.config.AuthorizationHeader);

            return await Task.Run(async () => {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(model.AsQuery());
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var responseModel = new ResponseModel();
                try
                {//Exception occur here as a result of Insufficient credit balance, Invalid phone number or internet connectivity failure
                    using (var httpResponse = (HttpWebResponse)request.GetResponse())
                    {
                        using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            string responseAsText = sr.ReadToEnd();
                            Console.WriteLine(responseAsText);
                            responseModel = JsonConvert.DeserializeObject<ResponseModel>(responseAsText);
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse webResponse = (HttpWebResponse)ex.Response;
                    if (webResponse != null)
                    {
                        if (webResponse.StatusCode == HttpStatusCode.BadRequest)
                        {//Occur as a result of Insufficient credit balance or invalid number 
                            var checkBalance = await GetCredits();
                            responseModel.Status = checkBalance.SmsCredits <= 3 ? "Insufficient credit" : "Invalid number";
                        }
                        else
                        {// Any other error that may occur
                            responseModel.Status = "Unknown error";
                        }
                    }
                    else //When there is internet connectivity failure 'webResponse' will be equal to null
                    {
                        responseModel.Status = "Internet problem";
                    }
                }
                return responseModel;
            });
        }

        public async Task<CreditModel> GetCredits() {
            var request = (HttpWebRequest)WebRequest.Create(this.config.ResolveURL("get_credits"));
            request.Method = Constants.GET;
            request.ContentType = Constants.JSON;
            request.Credentials = this.config.Credentials;
            request.Headers.Add("Authorization", this.config.AuthorizationHeader);
            return await Task.Run(() => {
                using (var httpResponse = (HttpWebResponse)request.GetResponse()) {
                    using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream())) {
                        string responseAsText = sr.ReadToEnd();
                        Console.WriteLine(responseAsText);
                        CreditModel responseModel = JsonConvert.DeserializeObject<CreditModel>(responseAsText);
                        return responseModel;
                    }
                }
            });
        }

        public async Task<DeliveryStatusModel> GetDeliveryStatus(string messageId) {
            var request = (HttpWebRequest)WebRequest.Create(this.config.ResolveURL("delivery_status?message_id=" + messageId));
            request.Method = Constants.GET;
            request.ContentType = Constants.JSON;
            request.Credentials = this.config.Credentials;
            request.Headers.Add("Authorization", this.config.AuthorizationHeader);
            return await Task.Run(() => {
                using (var httpResponse = (HttpWebResponse)request.GetResponse()) {
                    using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream())) {
                        string responseAsText = sr.ReadToEnd();
                        Console.WriteLine(responseAsText);
                        DeliveryStatusModel responseModel = JsonConvert.DeserializeObject<DeliveryStatusModel>(responseAsText);
                        return responseModel;
                    }
                }
            });
        }
    }
}
