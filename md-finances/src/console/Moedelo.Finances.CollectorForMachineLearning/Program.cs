using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject;
using Moedelo.InfrastructureV2.Logging;

namespace Moedelo.Finances.CollectorForMachineLearning
{
    internal class Program
    {
        private const string TAG = nameof(Program);

        static void Main()
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var installer = new DefaultDIInstaller(new Logger(), false, false);
            installer.Initialize();
            var logger = installer.GetInstance<ILogger>();
            try
            {
                var console = installer.GetInstance<Console>();
                await console.RunAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.Error(TAG, ex.Message, ex);
                throw;
            }
        }
    }
}
