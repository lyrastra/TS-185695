using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(TransferFromCashApiClient))]
    class TransferFromCashApiClient
    {
        private const string path = "Incoming/TransferFromCash";

        private readonly IPaymentOrderApiClient apiClient;

        public TransferFromCashApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<TransferFromCashResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<TransferFromCashDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return TransferFromCashMapper.MapToResponse(dto);
        }

        public Task CreateAsync(TransferFromCashSaveRequest request)
        {
            var dto = TransferFromCashMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(TransferFromCashSaveRequest request)
        {
            var dto = TransferFromCashMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
