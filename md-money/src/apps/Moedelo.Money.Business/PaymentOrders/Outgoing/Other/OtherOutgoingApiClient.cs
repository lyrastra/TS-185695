using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [InjectAsSingleton(typeof(OtherOutgoingApiClient))]
    internal sealed class OtherOutgoingApiClient
    {
        private const string path = "Outgoing/Other";

        private readonly IPaymentOrderApiClient apiClient;

        public OtherOutgoingApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<OtherOutgoingResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<OtherOutgoingDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return OtherOutgoingMapper.MapToResponse(dto);
        }

        public Task CreateAsync(OtherOutgoingSaveRequest request)
        {
            var dto = OtherOutgoingMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(OtherOutgoingSaveRequest request)
        {
            var dto = OtherOutgoingMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
