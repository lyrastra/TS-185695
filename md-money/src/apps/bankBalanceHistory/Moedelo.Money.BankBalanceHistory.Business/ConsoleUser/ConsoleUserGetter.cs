using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.ConsoleUser;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.BankBalanceHistory.Business.ConsoleUser
{
    [InjectAsSingleton(typeof(IConsoleUserGetter))]
    class ConsoleUserGetter : IConsoleUserGetter
    {
        private readonly IConsoleUserApiClient consoleUserApiClient;
        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private int? userId;

        public ConsoleUserGetter(IConsoleUserApiClient consoleUserApiClient)
        {
            this.consoleUserApiClient = consoleUserApiClient;
        }

        public async Task<int> GetConsoleUserId()
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                if (userId == null)
                {
                    var user = await consoleUserApiClient.GetOrCreateByLoginAsync("Moedelo.Money.BankBalanceHistory");
                    userId = user.Id;
                }
                return userId.Value;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
