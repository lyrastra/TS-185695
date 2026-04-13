using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Providing.ApiClient.Abstractions.ProvidingState;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(PaymentOrderProvidingStateSetter))]
    internal sealed class PaymentOrderProvidingStateSetter
    {
        private readonly IProvidingStateApiClient providingStateApiClient;

        public PaymentOrderProvidingStateSetter(IProvidingStateApiClient providingStateApiClient)
        {
            this.providingStateApiClient = providingStateApiClient;
        }

        public Task UnsetStateAsync(long stateId)
        {
            if (stateId == 0)
            {
                return Task.CompletedTask;
            }
            
            return providingStateApiClient.UnsetAsync(stateId);
        }
        
        public Task UnsetByBaseIdAsync(long baseId)
        {
            if (baseId == 0)
            {
                return Task.CompletedTask;
            }
            
            return providingStateApiClient.UnsetByBaseIdAsync(baseId);
        }
    }
}
