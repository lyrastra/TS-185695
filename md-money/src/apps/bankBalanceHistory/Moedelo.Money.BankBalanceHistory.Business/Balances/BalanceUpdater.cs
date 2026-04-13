using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances;
using Moedelo.Money.BankBalanceHistory.DataAccess.Abstractions;
using Moedelo.Money.BankBalanceHistory.Domain;

namespace Moedelo.Money.BankBalanceHistory.Business.Balances
{
    [InjectAsSingleton(typeof(IBalanceUpdater))]
    public class BalanceUpdater : IBalanceUpdater
    {
        private readonly IBalancesDao balancesDao;
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public BalanceUpdater(IBalancesDao balancesDao, 
            IExecutionInfoContextAccessor contextAccessor)
        {
            this.balancesDao = balancesDao;
            this.contextAccessor = contextAccessor;
        }

        public async Task UpdateAsync(IReadOnlyCollection<BankBalanceUpdateRequest> requests)
        {
            var context = contextAccessor.ExecutionInfoContext;

            await balancesDao.UpdateAsync((int)context.FirmId, requests);
        }
    }
}