using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract
{
    [InjectAsSingleton(typeof(AgencyContractApiClient))]
    internal sealed class AgencyContractApiClient
    {
        private const string path = "Outgoing/AgencyContract";

        private readonly IPaymentOrderApiClient apiClient;

        public AgencyContractApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<AgencyContractResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<AgencyContractDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return AgencyContractMapper.MapToResponse(dto);
        }

        public Task CreateAsync(AgencyContractSaveRequest request)
        {
            var dto = AgencyContractMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(AgencyContractSaveRequest request)
        {
            var dto = AgencyContractMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
