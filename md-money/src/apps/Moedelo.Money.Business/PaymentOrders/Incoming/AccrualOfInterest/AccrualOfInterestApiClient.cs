using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    [InjectAsSingleton(typeof(AccrualOfInterestApiClient))]
    internal sealed class AccrualOfInterestApiClient
    {
        private const string path = "Incoming/AccrualOfInterest";

        private readonly IPaymentOrderApiClient apiClient;

        public AccrualOfInterestApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<AccrualOfInterestResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<AccrualOfInterestDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return AccrualOfInterestMapper.MapToResponse(dto);
        }

        public Task CreateAsync(AccrualOfInterestSaveRequest request)
        {
            var dto = AccrualOfInterestMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(AccrualOfInterestSaveRequest request)
        {
            var dto = AccrualOfInterestMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
