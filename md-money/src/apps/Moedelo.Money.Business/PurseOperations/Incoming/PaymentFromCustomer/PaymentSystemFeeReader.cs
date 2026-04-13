using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PurseOperations.Incoming.PaymentFromCustomer;
using Moedelo.Money.Domain.PurseOperations.Incoming.PaymentFromCustomer;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PurseOperations.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerReader))]
    internal class PaymentFromCustomerReader : IPaymentFromCustomerReader
    {
        private readonly PaymentFromCustomerApiClient apiClient;

        public PaymentFromCustomerReader(
            PaymentFromCustomerApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public Task<PaymentFromCustomerResponse> GetByBaseIdAsync(long documentBaseId)
        {
            return apiClient.GetAsync(documentBaseId);
        }
    }
}
