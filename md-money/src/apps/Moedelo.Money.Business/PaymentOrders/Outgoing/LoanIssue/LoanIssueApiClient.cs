using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanIssue
{
    [InjectAsSingleton(typeof(LoanIssueApiClient))]
    internal sealed class LoanIssueApiClient
    {
        private const string path = "Outgoing/LoanIssue";

        private readonly IPaymentOrderApiClient apiClient;

        public LoanIssueApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<LoanIssueResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<LoanIssueDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return LoanIssueMapper.MapToResponse(dto);
        }

        public Task CreateAsync(LoanIssueSaveRequest request)
        {
            var dto = LoanIssueMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(LoanIssueSaveRequest request)
        {
            var dto = LoanIssueMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
