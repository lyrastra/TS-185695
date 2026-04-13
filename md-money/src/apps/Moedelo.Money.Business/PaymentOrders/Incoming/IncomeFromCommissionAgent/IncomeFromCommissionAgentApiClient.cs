using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    [InjectAsSingleton(typeof(IncomeFromCommissionAgentApiClient))]
    internal sealed class IncomeFromCommissionAgentApiClient
    {
        private const string path = "Incoming/IncomeFromCommissionAgent";

        private readonly IPaymentOrderApiClient apiClient;

        public IncomeFromCommissionAgentApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IncomeFromCommissionAgentResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<IncomeFromCommissionAgentDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return IncomeFromCommissionAgentMapper.MapToResponse(dto);
        }

        public Task CreateAsync(IncomeFromCommissionAgentSaveRequest request)
        {
            var dto = IncomeFromCommissionAgentMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(IncomeFromCommissionAgentSaveRequest request)
        {
            var dto = IncomeFromCommissionAgentMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
