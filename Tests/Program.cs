using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jusibe;
using Jusibe.Models;
using Extensions;

namespace Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Client client = new Client();
            client.GetCredits().Success((Credit credit) =>
            {
                string jsonResponse = credit.ToJson(true);
                jsonResponse.SaveToFile("credits.json");
                Console.WriteLine(jsonResponse);
            }).Error((Exception ex) =>
            {
                throw ex;
            });

            SMS.Request request = new SMS.Request("08083850091", "mykeels", "You Rock!");
            Console.WriteLine(request.ToJson(true));
            client.SendSms(request).Success((SMS.Response response) =>
            {
                string jsonResponse = response.ToJson(true);
                jsonResponse.SaveToFile("sms-delivery.json");
                Console.WriteLine(jsonResponse);
            }).Error((Exception ex) =>
            {
                throw ex;
            });

            Console.Read();
        }
    }
}
