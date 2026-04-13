using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    [InjectAsSingleton(typeof(WithdrawalFromAccountApiClient))]
    internal sealed class WithdrawalFromAccountApiClient
    {
        private const string path = "Outgoing/WithdrawalFromAccount";

        private readonly IPaymentOrderApiClient apiClient;

        public WithdrawalFromAccountApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<WithdrawalFromAccountResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<WithdrawalFromAccountDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return WithdrawalFromAccountMapper.MapToResponse(dto);
        }

        public Task CreateAsync(WithdrawalFromAccountSaveRequest request)
        {
            var dto = WithdrawalFromAccountMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(WithdrawalFromAccountSaveRequest request)
        {
            var dto = WithdrawalFromAccountMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
