using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanObtaining
{
    [InjectAsSingleton(typeof(LoanObtainingApiClient))]
    internal sealed class LoanObtainingApiClient
    {
        private const string path = "Incoming/LoanObtaining";

        private readonly IPaymentOrderApiClient apiClient;

        public LoanObtainingApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<LoanObtainingResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<LoanObtainingDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return LoanObtainingMapper.MapToResponse(dto);
        }

        public Task CreateAsync(LoanObtainingSaveRequest request)
        {
            var dto = LoanObtainingMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(LoanObtainingSaveRequest request)
        {
            var dto = LoanObtainingMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
