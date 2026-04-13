using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    [InjectAsSingleton(typeof(WithdrawalOfProfitApiClient))]
    internal sealed class WithdrawalOfProfitApiClient
    {
        private const string path = "Outgoing/WithdrawalOfProfit";

        private readonly IPaymentOrderApiClient apiClient;

        public WithdrawalOfProfitApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<WithdrawalOfProfitResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<WithdrawalOfProfitDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return WithdrawalOfProfitMapper.MapToResponse(dto);
        }

        public Task CreateAsync(WithdrawalOfProfitSaveRequest request)
        {
            var dto = WithdrawalOfProfitMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(WithdrawalOfProfitSaveRequest request)
        {
            var dto = WithdrawalOfProfitMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
