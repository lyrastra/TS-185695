using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromPurse
{
    [InjectAsSingleton(typeof(TransferFromPurseApiClient))]
    class TransferFromPurseApiClient
    {
        private const string path = "Incoming/TransferFromPurse";

        private readonly IPaymentOrderApiClient apiClient;

        public TransferFromPurseApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<TransferFromPurseResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<TransferFromPurseDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return TransferFromPurseMapper.MapToResponse(dto);
        }

        public Task CreateAsync(TransferFromPurseSaveRequest request)
        {
            var dto = TransferFromPurseMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(TransferFromPurseSaveRequest request)
        {
            var dto = TransferFromPurseMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
