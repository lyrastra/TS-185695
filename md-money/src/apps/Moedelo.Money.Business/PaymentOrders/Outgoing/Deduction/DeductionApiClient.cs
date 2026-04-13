using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(DeductionApiClient))]
    internal sealed class DeductionApiClient
    {
        private const string path = "Outgoing/Deduction";

        private readonly IPaymentOrderApiClient apiClient;

        public DeductionApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<DeductionResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<DeductionDto>($"{path}/{documentBaseId}");
            return DeductionMapper.MapToResponse(dto);
        }

        public Task CreateAsync(DeductionSaveRequest request)
        {
            var dto = DeductionMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(DeductionSaveRequest request)
        {
            var dto = DeductionMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
