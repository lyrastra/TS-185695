using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.CashOrders.ApiClient;
using Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(UnifiedBudgetaryPaymentApiClient))]
    internal class UnifiedBudgetaryPaymentApiClient
    {
        private const string path = "Outgoing/UnifiedBudgetaryPayment";

        private readonly ICashOrderApiClient apiClient;

        public UnifiedBudgetaryPaymentApiClient(
            ICashOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<UnifiedBudgetaryPaymentResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<UnifiedBudgetaryPaymentDto>($"{path}/{documentBaseId}");
            return UnifiedBudgetaryPaymentMapper.MapToResponse(dto);
        }

        public Task CreateAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            var dto = UnifiedBudgetaryPaymentMapper.MapToDto(request);
            return apiClient.CreateAsync($"{path}", dto);
        }

        public async Task<UnifiedBudgetaryPaymentSaveResponse> UpdateAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            var dto = UnifiedBudgetaryPaymentMapper.MapToDto(request);
            var response = await apiClient.UpdateAsync<UnifiedBudgetaryPaymentDto, UnifiedBudgetaryPaymentUpdateResponseDto>(
                $"{path}/{request.DocumentBaseId}", dto);
            return UnifiedBudgetaryPaymentMapper.MapToResponse(response);
        }

        public async Task<UnifiedBudgetaryPaymentDeleteResponse> DeleteAsync(long documentBaseId)
        {
            var response = await apiClient.DeleteAsync<UnifiedBudgetaryPaymentDeleteResponseDto>(
                $"{path}/{documentBaseId}");
            return UnifiedBudgetaryPaymentMapper.MapToResponse(response);
        }
    }
}
