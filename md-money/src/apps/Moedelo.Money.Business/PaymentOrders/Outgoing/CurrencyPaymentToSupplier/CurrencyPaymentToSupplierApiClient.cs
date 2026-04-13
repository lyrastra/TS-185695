using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(CurrencyPaymentToSupplierApiClient))]
    internal sealed class CurrencyPaymentToSupplierApiClient
    {
        private const string path = "Outgoing/CurrencyPaymentToSupplier";

        private readonly IPaymentOrderApiClient apiClient;

        public CurrencyPaymentToSupplierApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<CurrencyPaymentToSupplierResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<CurrencyPaymentToSupplierDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return CurrencyPaymentToSupplierMapper.MapToResponse(dto);
        }

        public Task CreateAsync(CurrencyPaymentToSupplierSaveRequest request)
        {
            var dto = CurrencyPaymentToSupplierMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(CurrencyPaymentToSupplierSaveRequest request)
        {
            var dto = CurrencyPaymentToSupplierMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
