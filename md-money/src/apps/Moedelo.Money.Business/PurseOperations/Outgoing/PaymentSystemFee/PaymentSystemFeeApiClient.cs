using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PurseOperations.ApiClient;
using Moedelo.Money.Domain.PurseOperations.Outgoing.PaymentSystemFee;
using Moedelo.Money.PurseOperations.Dto.PurseOperations.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PurseOperations.Outgoing.PaymentSystemFee
{
    [InjectAsSingleton(typeof(PaymentSystemFeeApiClient))]
    internal sealed class PaymentSystemFeeApiClient
    {
        private const string path = "Outgoing/PaymentSystemFee";

        private readonly IPurseOperationApiClient apiClient;

        public PaymentSystemFeeApiClient(
            IPurseOperationApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<PaymentSystemFeeResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<PaymentSystemFeeDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return PaymentSystemFeeMapper.MapToResponse(dto);
        }
    }
}
