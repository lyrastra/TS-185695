using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyBankFee
{
    [InjectAsSingleton(typeof(CurrencyBankFeeApiClient))]
    internal sealed class CurrencyBankFeeApiClient
    {
        private const string path = "Outgoing/CurrencyBankFee";

        private readonly IPaymentOrderApiClient apiClient;

        public CurrencyBankFeeApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<CurrencyBankFeeResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<CurrencyBankFeeDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return CurrencyBankFeeMapper.MapToResponse(dto);
        }

        public Task CreateAsync(CurrencyBankFeeSaveRequest request)
        {
            var dto = CurrencyBankFeeMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(CurrencyBankFeeSaveRequest request)
        {
            var dto = CurrencyBankFeeMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}