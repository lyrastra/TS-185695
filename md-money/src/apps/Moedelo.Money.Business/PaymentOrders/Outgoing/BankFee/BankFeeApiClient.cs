using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BankFee
{
    [InjectAsSingleton(typeof(BankFeeApiClient))]
    internal sealed class BankFeeApiClient
    {
        private const string path = "Outgoing/BankFee";

        private readonly IPaymentOrderApiClient apiClient;

        public BankFeeApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<BankFeeResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<BankFeeDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return BankFeeMapper.MapToResponse(dto);
        }

        public Task CreateAsync(BankFeeSaveRequest request)
        {
            var dto = BankFeeMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(BankFeeSaveRequest request)
        {
            var dto = BankFeeMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
