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
            /*client.GetCredits().Success((Credit credit) =>
            {
                string jsonResponse = credit.ToJson(true);
                jsonResponse.SaveToFile("credits.json");
                Console.WriteLine(jsonResponse);
            }).Error((Exception ex) =>
            {
                throw ex;
            });*/

            /*SMS.Requests requests = new SMS.Requests(new List<string>() { "08083850091", "08054139218" }, "Igbo Overlords!", "Testing!!!");
            client.SendSms(requests).Success((responses) =>
            {
                responses.ForEach((response) => Console.Write(response.ToJson(true)));
            }).Error((Exception ex) =>
            {
                throw ex;
            });
            Console.Read();*/

            SMS.Request request = new SMS.Request("08054139218", "Overlord!", @"You SUCK!!!");
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

            /*client.CheckDelivery("nw53j49123").Success((SMS.DeliveryStatus status) =>
            {
                string jsonResponse = status.ToJson(true);
                jsonResponse.SaveToFile("sms-delivery-status.json");
                Console.WriteLine(jsonResponse);
            }).Error((Exception ex) =>
            {
                throw ex;
            });*/

            Console.Read();
        }
    }
}
