using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    [InjectAsSingleton(typeof(RefundToSettlementAccountApiClient))]
    internal sealed class RefundToSettlementAccountApiClient
    {
        private const string path = "Incoming/RefundToSettlementAccount";

        private readonly IPaymentOrderApiClient apiClient;

        public RefundToSettlementAccountApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<RefundToSettlementAccountResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<RefundToSettlementAccountDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return RefundToSettlementAccountMapper.MapToResponse(dto);
        }

        public Task CreateAsync(RefundToSettlementAccountSaveRequest request)
        {
            var dto = RefundToSettlementAccountMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(RefundToSettlementAccountSaveRequest request)
        {
            var dto = RefundToSettlementAccountMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
