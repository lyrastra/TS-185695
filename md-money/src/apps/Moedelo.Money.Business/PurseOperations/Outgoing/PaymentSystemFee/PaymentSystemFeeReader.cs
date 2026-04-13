using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PurseOperations.Outgoing.PaymentSystemFee;
using Moedelo.Money.Domain.PurseOperations.Outgoing.PaymentSystemFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PurseOperations.Outgoing.PaymentSystemFee
{
    [InjectAsSingleton(typeof(IPaymentSystemFeeReader))]
    internal class PaymentSystemFeeReader : IPaymentSystemFeeReader
    {
        private readonly PaymentSystemFeeApiClient apiClient;

        public PaymentSystemFeeReader(
            PaymentSystemFeeApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public Task<PaymentSystemFeeResponse> GetByBaseIdAsync(long documentBaseId)
        {
            return apiClient.GetAsync(documentBaseId);
        }
    }
}
