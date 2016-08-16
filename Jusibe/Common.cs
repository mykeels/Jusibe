using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Jusibe
{
    public class Common
    {
        public static string rootUrl = ConfigurationManager.AppSettings["Jusibe_Root_Url"];
    }
}
