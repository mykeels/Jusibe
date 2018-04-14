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
            var request = HttpWebRequest.Create(this.config.ResolveURL("send_sms"));
            request.Method = Constants.POST;
            request.ContentType = Constants.X_WWW_FORM_URL_ENCODED;
            request.Credentials = this.config.Credentials;
            return await Task.Run(() => {
                using (var httpResponse = (HttpWebResponse)request.GetResponse()) {
                    using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream())) {
                        string responseAsText = sr.ReadToEnd();
                        ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(responseAsText);
                        return responseModel;
                    }
                }
            });
        }
    }
}
