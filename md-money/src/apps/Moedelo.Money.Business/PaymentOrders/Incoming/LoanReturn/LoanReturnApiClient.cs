using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn
{
    [InjectAsSingleton(typeof(LoanReturnApiClient))]
    internal sealed class LoanReturnApiClient
    {
        private const string path = "Incoming/LoanReturn";

        private readonly IPaymentOrderApiClient apiClient;

        public LoanReturnApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<LoanReturnResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<LoanReturnDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return LoanReturnMapper.MapToResponse(dto);
        }

        public Task CreateAsync(LoanReturnSaveRequest request)
        {
            var dto = LoanReturnMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(LoanReturnSaveRequest request)
        {
            var dto = LoanReturnMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
