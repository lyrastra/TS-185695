using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer
{
    [InjectAsSingleton(typeof(RefundToCustomerApiClient))]
    internal sealed class RefundToCustomerApiClient
    {
        private const string path = "Outgoing/RefundToCustomer";

        private readonly IPaymentOrderApiClient apiClient;

        public RefundToCustomerApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<RefundToCustomerResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<RefundToCustomerDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return RefundToCustomerMapper.MapToResponse(dto);
        }

        public Task CreateAsync(RefundToCustomerSaveRequest request)
        {
            var dto = RefundToCustomerMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(RefundToCustomerSaveRequest request)
        {
            var dto = RefundToCustomerMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
