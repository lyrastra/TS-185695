using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [InjectAsSingleton(typeof(CurrencyTransferToAccountApiClient))]
    internal sealed class CurrencyTransferToAccountApiClient
    {
        private const string path = "Outgoing/CurrencyTransferToAccount";

        private readonly IPaymentOrderApiClient apiClient;

        public CurrencyTransferToAccountApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<CurrencyTransferToAccountResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<CurrencyTransferToAccountDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return CurrencyTransferToAccountMapper.MapToResponse(dto);
        }

        public Task CreateAsync(CurrencyTransferToAccountDto dto)
        {
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(CurrencyTransferToAccountDto dto)
        {
            return apiClient.UpdateAsync($"{path}/{dto.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
