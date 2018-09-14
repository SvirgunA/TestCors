using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TestCors.API.Configure;

namespace TestCors.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.SeedDatabase();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
