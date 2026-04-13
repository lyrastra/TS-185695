using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [InjectAsSingleton(typeof(CurrencyPaymentFromCustomerApiClient))]
    internal sealed class CurrencyPaymentFromCustomerApiClient
    {
        private const string path = "Incoming/CurrencyPaymentFromCustomer";

        private readonly IPaymentOrderApiClient apiClient;

        public CurrencyPaymentFromCustomerApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<CurrencyPaymentFromCustomerResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<CurrencyPaymentFromCustomerDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return CurrencyPaymentFromCustomerMapper.MapToResponse(dto);
        }

        public Task CreateAsync(CurrencyPaymentFromCustomerSaveRequest request)
        {
            var dto = CurrencyPaymentFromCustomerMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(CurrencyPaymentFromCustomerSaveRequest request)
        {
            var dto = CurrencyPaymentFromCustomerMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}