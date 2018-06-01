using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace rpi_garage_door
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()  
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("hosting.json", optional: true)
                            .Build();
            BuildWebHost(args, config) .Run();
        }

        public static IWebHost BuildWebHost(string[] args, IConfigurationRoot config) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();
    }
}
