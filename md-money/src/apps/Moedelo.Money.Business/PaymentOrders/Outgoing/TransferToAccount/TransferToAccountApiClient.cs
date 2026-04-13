using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(TransferToAccountApiClient))]
    internal sealed class TransferToAccountApiClient
    {
        private const string path = "Outgoing/TransferToAccount";

        private readonly IPaymentOrderApiClient apiClient;

        public TransferToAccountApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<TransferToAccountResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<TransferToAccountDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return TransferToAccountMapper.MapToResponse(dto);
        }

        public Task CreateAsync(TransferToAccountSaveRequest request)
        {
            var dto = TransferToAccountMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(TransferToAccountSaveRequest request)
        {
            var dto = TransferToAccountMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
