using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount
{
    [InjectAsSingleton(typeof(TransferFromAccountApiClient))]
    internal sealed class TransferFromAccountApiClient
    {
        private const string path = "Incoming/TransferFromAccount";

        private readonly IPaymentOrderApiClient apiClient;

        public TransferFromAccountApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<TransferFromAccountResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<TransferFromAccountDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return TransferFromAccountMapper.MapToResponse(dto);
        }

        public Task CreateAsync(TransferFromAccountSaveRequest request)
        {
            var dto = TransferFromAccountMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(TransferFromAccountSaveRequest request)
        {
            var dto = TransferFromAccountMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
