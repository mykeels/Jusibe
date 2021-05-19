using Jusibe.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace Jusibe
{
    public class JusibeClientOptions
    {
        public string Key { get; set; }
        public string Token { get; set; }
        public string BaseAddress { get; set; }
    }

    public static class JusibeExtensions
    {
        public static void AddJusibeClient(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new JusibeClientOptions();
            var section = configuration.GetSection("Jusibe");
            section.Bind(options);
            services.Configure<JusibeClientOptions>(section);

            services.AddHttpClient("jusibe", client =>
            {
                client.BaseAddress = new Uri(options.BaseAddress);
                client.DefaultRequestHeaders.Add("ContentType", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{options.Key}:{options.Token}"))}");
            });

            services.AddSingleton<IJusibeClient, JusibeClient>();
        }
    }
}
