using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment
{
    [InjectAsSingleton(typeof(LoanRepaymentApiClient))]
    internal sealed class LoanRepaymentApiClient
    {
        private const string path = "Outgoing/LoanRepayment";

        private readonly IPaymentOrderApiClient apiClient;

        public LoanRepaymentApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<LoanRepaymentResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<LoanRepaymentDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return LoanRepaymentMapper.MapToResponse(dto);
        }

        public Task CreateAsync(LoanRepaymentSaveRequest request)
        {
            var dto = LoanRepaymentMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(LoanRepaymentSaveRequest request)
        {
            var dto = LoanRepaymentMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
