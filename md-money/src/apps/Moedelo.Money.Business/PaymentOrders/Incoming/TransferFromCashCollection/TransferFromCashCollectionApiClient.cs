using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCashCollection
{
    [InjectAsSingleton(typeof(TransferFromCashCollectionApiClient))]
    internal sealed class TransferFromCashCollectionApiClient
    {
        private const string path = "Incoming/TransferFromCashCollection";

        private readonly IPaymentOrderApiClient apiClient;

        public TransferFromCashCollectionApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<TransferFromCashCollectionResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<TransferFromCashCollectionDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return TransferFromCashCollectionMapper.MapToResponse(dto);
        }

        public Task CreateAsync(TransferFromCashCollectionSaveRequest request)
        {
            var dto = TransferFromCashCollectionMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(TransferFromCashCollectionSaveRequest request)
        {
            var dto = TransferFromCashCollectionMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
