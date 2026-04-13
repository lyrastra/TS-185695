using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.CashOrders.ApiClient;
using Moedelo.Money.Domain.CashOrders.Incoming.RetailRevenue;
using Moedelo.Money.CashOrders.Dto.CashOrders.Incoming;
using System.Threading.Tasks;
using Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing;

namespace Moedelo.Money.Business.CashOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(RetailRevenueApiClient))]
    internal class RetailRevenueApiClient
    {
        private const string path = "Incoming/RetailRevenue";

        private readonly ICashOrderApiClient apiClient;

        public RetailRevenueApiClient(
            ICashOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<RetailRevenueResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<RetailRevenueDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return RetailRevenueMapper.MapToResponse(dto);
        }

        public Task UpdateAsync(RetailRevenueSaveRequest request)
        {
            var dto = RetailRevenueMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }
    }
}
