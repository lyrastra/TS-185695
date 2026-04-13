using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    [InjectAsSingleton(typeof(CurrencyOtherOutgoingApiClient))]
    internal sealed class CurrencyOtherOutgoingApiClient
    {
        private const string path = "Outgoing/CurrencyOther";

        private readonly IPaymentOrderApiClient apiClient;

        public CurrencyOtherOutgoingApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<CurrencyOtherOutgoingResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<CurrencyOtherOutgoingDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return CurrencyOtherOutgoingMapper.MapToResponse(dto);
        }

        public Task CreateAsync(CurrencyOtherOutgoingSaveRequest request)
        {
            var dto = CurrencyOtherOutgoingMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(CurrencyOtherOutgoingSaveRequest request)
        {
            var dto = CurrencyOtherOutgoingMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}