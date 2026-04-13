using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Billing.Abstractions.Legacy.Interfaces;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Operations;
using Moedelo.Money.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Operations
{
    [InjectAsSingleton(typeof(IOperationsAccessReader))]
    internal class OperationsAccessReader : IOperationsAccessReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IFirmBillingStateApiClient billingStateApiClient;

        public OperationsAccessReader(
            IExecutionInfoContextAccessor contextAccessor, 
            IFirmBillingStateApiClient billingStateApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.billingStateApiClient = billingStateApiClient;
        }

        public async Task<OperationsAccessModel> GetAsync()
        {
            return new OperationsAccessModel
            {
                CanEditCurrencyOperations = await CanEditCurrencyOperations() 
            };
        }

        public async Task<bool> CanEditCurrencyOperations()
        {
            return contextAccessor.ExecutionInfoContext.UserRules.Contains(AccessRule.AccessToCurrencySettlementAccount)
                   || await IsTrialAsync();
        }

        private async Task<bool> IsTrialAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var state = await billingStateApiClient.GetActualAsync(context.FirmId);
            return state.PaidStatus == FirmBillingStateDto.FirmPaidStatus.Trial;
        }
    }
}