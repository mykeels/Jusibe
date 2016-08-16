using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jusibe.Models
{
    public class SMS
    {

        public static string GetEndpointUrl()
        {
            string root_url = Common.rootUrl;
            return root_url.TrimEnd('/') + "send_sms";
        }

        public class Request
        {
            public string to { get; set; }
            public string from { get; set; }
            public int message { get; set; }
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
                return root_url.TrimEnd('/') + "delivery_status/?message_id=" + message_id;
            }
        }
    }
}
