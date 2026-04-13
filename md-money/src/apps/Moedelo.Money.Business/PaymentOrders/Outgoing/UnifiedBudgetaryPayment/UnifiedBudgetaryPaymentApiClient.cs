using Moedelo.Authorities.ApiClient.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentApiClient))]
    internal sealed class UnifiedBudgetaryPaymentApiClient(
        IPaymentOrderApiClient apiClient) : IUnifiedBudgetaryPaymentApiClient
    {
        private const string path = "Outgoing/UnifiedBudgetaryPayment";

        public async Task<UnifiedBudgetaryPaymentResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<UnifiedBudgetaryPaymentDto>($"{path}/{documentBaseId}");
            return UnifiedBudgetaryPaymentMapper.MapToResponse(dto);
        }

        public Task CreateAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            var dto = UnifiedBudgetaryPaymentMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public async Task<UnifiedBudgetaryPaymentSaveResponse> UpdateAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            var dto = UnifiedBudgetaryPaymentMapper.MapToDto(request);
            var response = await apiClient.UpdateAsync<UnifiedBudgetaryPaymentDto, ApiDataResult<UnifiedBudgetaryPaymentUpdateResponseDto>>(
                $"{path}/{request.DocumentBaseId}", dto);
            return UnifiedBudgetaryPaymentMapper.MapToResponse(response.data);
        }

        public async Task<UnifiedBudgetaryPaymentDeleteResponse> DeleteAsync(long documentBaseId)
        {
            var response = await apiClient.DeleteAsync<UnifiedBudgetaryPaymentDeleteResponseDto>(
                $"{path}/{documentBaseId}");
            return UnifiedBudgetaryPaymentMapper.MapToResponse(response);
        }

        public async Task<long> GetSubPaymentParentIdAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<ParentIdDto>($"{path}/SubPayments/{documentBaseId}/ParentId");
            return dto.ParentId;
        }

        public async Task UpdateTaxPostingTypeAsync(long documentBaseId, ProvidePostingType taxPostingType)
        {
            await apiClient.PutAsync($"{path}/SubPayments/{documentBaseId}/TaxPostingType", new { TaxPostingType = (int)taxPostingType });
        }

        public async Task<UnifiedBudgetarySubPayment[]> GetSubPaymentsByParentIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var response = await apiClient.PostAsync<IReadOnlyCollection<long>, UnifiedBudgetarySubPaymentDto[]>(
                $"{path}/SubPayments/GetByParentIds", documentBaseIds);
            return response.Select(UnifiedBudgetaryPaymentMapper.MapToResponse).ToArray();
        }

        public async Task<UnifiedBudgetarySubPayment[]> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds, CancellationToken ct)
        {
            var response = await apiClient.PostAsync<IReadOnlyCollection<long>, UnifiedBudgetarySubPaymentDto[]>(
                $"{path}/SubPayments/GetByBaseIds", documentBaseIds, ct);
            return response.Select(UnifiedBudgetaryPaymentMapper.MapToResponse).ToArray();
        }
    }
}
