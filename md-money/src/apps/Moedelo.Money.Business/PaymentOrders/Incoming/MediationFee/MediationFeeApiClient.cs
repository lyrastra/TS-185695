using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.MediationFee
{
    [InjectAsSingleton(typeof(MediationFeeApiClient))]
    internal sealed class MediationFeeApiClient
    {
        private const string path = "Incoming/MediationFee";

        private readonly IPaymentOrderApiClient apiClient;

        public MediationFeeApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<MediationFeeResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<MediationFeeDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return MediationFeeMapper.MapToResponse(dto);
        }

        public Task CreateAsync(MediationFeeSaveRequest request)
        {
            var dto = MediationFeeMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(MediationFeeSaveRequest request)
        {
            var dto = MediationFeeMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
