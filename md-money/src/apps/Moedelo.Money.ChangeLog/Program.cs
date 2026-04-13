using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Moedelo.Common.Logging.Configuration;

namespace Moedelo.Money.ChangeLog
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunAsync();
        }
        
        private static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureMoedeloCommonLogger("Moedelo.Money.ChangeLog")
                //.UseShutdownTimeout(TimeSpan.FromSeconds(30))
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
