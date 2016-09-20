using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Jusibe.Models
{
    public class SMS
    {

        public static string GetEndpointUrl()
        {
            string root_url = Common.rootUrl;
            return root_url.TrimEnd('/') + "/send_sms";
        }

        public class Requests: List<Request>
        {
            public Requests()
            {

            }

            public Requests(List<string> recipients, string from, string message)
            {
                recipients.ForEach((r) =>
                {
                    this.Add(new Request(r, from, message));
                });
            }
        }

        public class Request
        {
            public string to { get; set; }
            public string from { get; set; }
            public string message { get; set; }

            public Request()
            {

            }

            public Request(string to, string from, string message)
            {
                this.to = to;
                this.from = from;
                this.message = message;
            }

            public string getAsQuery()
            {
                return "?to=" + HttpUtility.UrlEncode(this.to) + 
                    "&from=" + HttpUtility.UrlEncode(this.from) + 
                    "&message=" + HttpUtility.UrlEncode(this.message);
            }

            public string getAsBody()
            {
                return "to=" + HttpUtility.UrlEncode(this.to) +
                    "&from=" + HttpUtility.UrlEncode(this.from) +
                    "&message=" + HttpUtility.UrlEncode(this.message);
            }
        }

        public class Response
        {
            public string status { get; set; }
            public string message_id { get; set; }
            public int sms_credits_used { get; set; }
        }

        public class DeliveryStatus
        {
            public string message_id { get; set; }
            public string status { get; set; }
            public string date_sent { get; set; }
            public string date_delivered { get; set; }
            public static string GetEndpointUrl(string message_id)
            {
                string root_url = Common.rootUrl;
                return root_url.TrimEnd('/') + "/delivery_status/?message_id=" + message_id;
            }
        }
    }
}
