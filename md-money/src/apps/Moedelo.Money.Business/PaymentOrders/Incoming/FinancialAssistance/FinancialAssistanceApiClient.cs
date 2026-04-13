using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.FinancialAssistance
{
    [InjectAsSingleton(typeof(FinancialAssistanceApiClient))]
    internal sealed class FinancialAssistanceApiClient
    {
        private const string path = "Incoming/FinancialAssistance";

        private readonly IPaymentOrderApiClient apiClient;

        public FinancialAssistanceApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<FinancialAssistanceResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<FinancialAssistanceDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return FinancialAssistanceMapper.MapToResponse(dto);
        }

        public Task CreateAsync(FinancialAssistanceSaveRequest request)
        {
            var dto = FinancialAssistanceMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(FinancialAssistanceSaveRequest request)
        {
            var dto = FinancialAssistanceMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
