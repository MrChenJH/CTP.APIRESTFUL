using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using CTP.Redis;

namespace CTP.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            string path = System.AppContext.BaseDirectory;

            var profile = new ConfigurationBuilder();
            var v = profile.AddJsonFile("profile.json");
            var p = v.Build();
        
            var value1 = p.GetValue<string>("profile:url1");
            var value2 = p.GetValue<string>("profile:url2");
            Profile.redisIp= p.GetValue<string>("profile:ip");
            var host = new WebHostBuilder()
                           .UseKestrel()
                           .UseContentRoot(Directory.GetCurrentDirectory())
                           .UseIISIntegration()
                           .UseStartup<Startup>()
                           .UseUrls(new string[] { value1, value2 })
                           .Build();

            host.Run();
        }
    }
}
