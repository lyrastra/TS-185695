using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Registry.Business.Abstractions;
using Moedelo.Money.Registry.Business.Models;

namespace Moedelo.Money.Registry.Business
{
    [InjectAsSingleton(typeof(IBalanceMasterService))]
    public class BalanceMasterService : IBalanceMasterService
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IBalancesApiClient balancesApiClient;

        public BalanceMasterService(
            IBalancesApiClient balancesApiClient, 
            IExecutionInfoContextAccessor executionInfoContext)
        {
            this.balancesApiClient = balancesApiClient;
            this.executionInfoContext = executionInfoContext;
        }

        public async Task<BalanceMasterStatus> GetStatusAsync()
        {
            var context = executionInfoContext.ExecutionInfoContext;

            var result = new BalanceMasterStatus();
            var date = await balancesApiClient.GetDateAsync(context.FirmId, context.UserId).ConfigureAwait(false);
            result.IsCompleted = date != null;
            if (date != null)
            {
                result.Date = date.Value;
            }
            return result;
        }
    }
}