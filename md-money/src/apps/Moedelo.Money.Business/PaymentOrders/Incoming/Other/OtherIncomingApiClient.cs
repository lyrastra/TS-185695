using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other
{
    [InjectAsSingleton(typeof(OtherIncomingApiClient))]
    internal sealed class OtherIncomingApiClient
    {
        private const string path = "Incoming/Other";

        private readonly IPaymentOrderApiClient apiClient;

        public OtherIncomingApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<OtherIncomingResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<OtherIncomingDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return OtherIncomingMapper.MapToResponse(dto);
        }

        public async Task<IReadOnlyCollection<OtherIncomingResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds)
        {
            var dtos = await apiClient.PostAsync<IReadOnlyCollection<long>, OtherIncomingDto[]>(
                $"{path}/ByBaseIds", documentBaseIds);
            return dtos.Select(OtherIncomingMapper.MapToResponse).ToArray();
        }

        public Task CreateAsync(OtherIncomingSaveRequest request)
        {
            var dto = OtherIncomingMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(OtherIncomingSaveRequest request)
        {
            var dto = OtherIncomingMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
