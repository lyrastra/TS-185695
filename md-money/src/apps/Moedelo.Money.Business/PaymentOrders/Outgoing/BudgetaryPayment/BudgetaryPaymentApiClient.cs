using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(BudgetaryPaymentApiClient))]
    internal sealed class BudgetaryPaymentApiClient
    {
        private const string path = "Outgoing/BudgetaryPayment";

        private readonly IPaymentOrderApiClient apiClient;

        public BudgetaryPaymentApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<BudgetaryPaymentResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<BudgetaryPaymentDto>($"{path}/{documentBaseId}");
            return BudgetaryPaymentMapper.MapToResponse(dto);
        }
        
        public async Task UpdateTaxPostingsModeAsync(long documentBaseId, bool isTaxPostingsInManualMode)
        {
            // желательно сделать одним вызовом
            var dto = await apiClient.GetAsync<BudgetaryPaymentDto>($"{path}/{documentBaseId}");

            dto.TaxPostingType = isTaxPostingsInManualMode
                ? ProvidePostingType.ByHand
                : ProvidePostingType.Auto;
            
            await apiClient.UpdateAsync($"{path}/{documentBaseId}", dto);
        }

        public Task CreateAsync(BudgetaryPaymentSaveRequest request)
        {
            var dto = BudgetaryPaymentMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(BudgetaryPaymentSaveRequest request)
        {
            var dto = BudgetaryPaymentMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }

        public async Task<BudgetaryPaymentReason[]> GetPaymentReasonsAsync()
        {
            var reasons = await apiClient.GetAsync<BudgetaryPaymentReasonDto[]>($"{path}/PaymentReasons");
            return BudgetaryPaymentMapper.MapToDomain(reasons);
        }

        public Task SetPayerKppAsync(long documentBaseId, string kpp)
        {
            return apiClient.PutAsync($"{path}/{documentBaseId}/Payer/Kpp", kpp);
        }
    }
}
