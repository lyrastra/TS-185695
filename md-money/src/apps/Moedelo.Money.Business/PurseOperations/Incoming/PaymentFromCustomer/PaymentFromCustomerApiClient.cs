using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PurseOperations.ApiClient;
using Moedelo.Money.Domain.PurseOperations.Incoming.PaymentFromCustomer;
using Moedelo.Money.PurseOperations.Dto.PurseOperations.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PurseOperations.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(PaymentFromCustomerApiClient))]
    internal sealed class PaymentFromCustomerApiClient
    {
        private const string path = "Outgoing/PaymentFromCustomer";

        private readonly IPurseOperationApiClient apiClient;

        public PaymentFromCustomerApiClient(
            IPurseOperationApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<PaymentFromCustomerResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<PaymentFromCustomerDto>($"{path}/{documentBaseId}");
            return PaymentFromCustomerMapper.MapToResponse(dto);
        }
    }
}
