using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(RetailRevenueApiClient))]
    internal sealed class RetailRevenueApiClient
    {
        private const string path = "Incoming/RetailRevenue";

        private readonly IPaymentOrderApiClient apiClient;

        public RetailRevenueApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<RetailRevenueResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<RetailRevenueDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return RetailRevenueMapper.MapToResponse(dto);
        }

        public Task CreateAsync(RetailRevenueSaveRequest request)
        {
            var dto = RetailRevenueMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(RetailRevenueSaveRequest request)
        {
            var dto = RetailRevenueMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
