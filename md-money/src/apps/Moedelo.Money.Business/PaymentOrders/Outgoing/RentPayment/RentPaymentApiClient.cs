using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(RentPaymentApiClient))]
    internal sealed class RentPaymentApiClient
    {
        private const string path = "Outgoing/RentPayment";
        private readonly IPaymentOrderApiClient apiClient;

        public RentPaymentApiClient(IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public Task CreateAsync(RentPaymentSaveRequest request)
        {
            var dto = RentPaymentMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public async Task<RentPaymentResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<RentPaymentDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return RentPaymentMapper.MapToResponse(dto);
        }

        public Task UpdateAsync(RentPaymentSaveRequest request)
        {
            var dto = RentPaymentMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
