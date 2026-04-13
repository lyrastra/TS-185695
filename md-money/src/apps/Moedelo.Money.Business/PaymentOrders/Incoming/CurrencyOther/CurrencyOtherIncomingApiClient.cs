using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyOther
{
    [InjectAsSingleton(typeof(CurrencyOtherIncomingApiClient))]
    internal sealed class CurrencyOtherIncomingApiClient
    {
        private const string path = "Incoming/CurrencyOther";

        private readonly IPaymentOrderApiClient apiClient;

        public CurrencyOtherIncomingApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<CurrencyOtherIncomingResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<CurrencyOtherIncomingDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return CurrencyOtherIncomingMapper.MapToResponse(dto);
        }

        public Task CreateAsync(CurrencyOtherIncomingSaveRequest request)
        {
            var dto = CurrencyOtherIncomingMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(CurrencyOtherIncomingSaveRequest request)
        {
            var dto = CurrencyOtherIncomingMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
