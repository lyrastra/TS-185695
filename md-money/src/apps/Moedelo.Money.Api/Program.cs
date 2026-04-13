using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Moedelo.Common.Logging.Configuration;
using Moedelo.Common.Logging.ExtraLog.Audit;
using Moedelo.Common.Logging.ExtraLog.ExecutionContext;
using Moedelo.Common.Logging.ExtraLog.HttpContext;
using System.Threading.Tasks;
using Moedelo.Common.Logging.ExtraLog.ExtraData;

namespace Moedelo.Money.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureMoedeloCommonLogger("Moedelo.Money.Api", builder =>
                {
                    builder.AddHttpContextExtraLogFields()
                        .AddAuditInfoContextExtraLogFields()
                        .AddExecutionInfoContextExtraLogFields()
                        .AddExtraDataContextExtraLogFields();
                })
                //.UseShutdownTimeout(TimeSpan.FromSeconds(30))
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}