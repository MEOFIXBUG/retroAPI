using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace retroAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseSystemd()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseKestrel(options =>
                    // {
                    //     //options.Listen(IPAddress.Loopback, 5000);  // http:localhost:5000
                    //     options.Listen(IPAddress.Any, 80);         // http:*:80
                    //     options.Listen(IPAddress.Any, 443, listenOptions =>
                    //     {
                    //         listenOptions.UseHttps("retroAPI.pfx", "a4e5a7c3-1eb8-4b23-944c-cd4d39ea1930");
                    //     });
                    // });
                     webBuilder.UseStartup<Startup>();
                });
    }
}
