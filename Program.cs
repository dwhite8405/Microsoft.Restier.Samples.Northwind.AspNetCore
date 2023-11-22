using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Restier.Samples.Northwind.AspNetCore
{
    /// <summary>
    /// Main method.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    /*
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.ConfigureEndpointDefaults(listenOptions =>
                        {
                            listenOptions.UseHttps(httpsOptions =>
                            {
                                // here you can configure SSL certificates and port numbers.
                            });
                        });
                    }); */

                    /* This is interesting. You can make this run faster and be
                     * more secure by using Unix sockets. Linux only.
                    
                    webBuilder.ConfigureKestrel((context, serverOptions) =>
                    {
                        serverOptions.ListenUnixSocket("/run/northwind.sock");

                        // there's also...
                        serverOptions.UseSystemd(); 
                    }); */

                    webBuilder.ConfigureKestrel(serverOptions => {
                       serverOptions.ListenLocalhost(5002);
                    });

                });
    }
}
