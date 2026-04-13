using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [InjectAsSingleton(typeof(CurrencyTransferFromAccountApiClient))]
    internal sealed class CurrencyTransferFromAccountApiClient
    {
        private const string path = "Incoming/CurrencyTransferFromAccount";

        private readonly IPaymentOrderApiClient apiClient;

        public CurrencyTransferFromAccountApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<CurrencyTransferFromAccountResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<CurrencyTransferFromAccountDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return CurrencyTransferFromAccountMapper.MapToResponse(dto);
        }

        public Task CreateAsync(CurrencyTransferFromAccountDto dto)
        {
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(CurrencyTransferFromAccountDto dto)
        {
            return apiClient.UpdateAsync($"{path}/{dto.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
