using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jusibe.Models
{
    public class Credit
    {
        public string sms_credits { get; set; }
        public static string GetEndpointUrl()
        {
            string root_url = Common.rootUrl;
            return root_url.TrimEnd('/') + "/get_credits";
        }
    }
}
