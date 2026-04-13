using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IncomingCurrencyPurchaseApiClient))]
    internal sealed class IncomingCurrencyPurchaseApiClient
    {
        private const string path = "Incoming/CurrencyPurchase";
        
        private readonly IPaymentOrderApiClient apiClient;

        public IncomingCurrencyPurchaseApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IncomingCurrencyPurchaseResponse> GetAsync(long documentBaseId)
        {
             var dto = await apiClient.GetAsync<IncomingCurrencyPurchaseDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
             return MapToResponse(dto);
        }
        
        private static IncomingCurrencyPurchaseResponse MapToResponse(IncomingCurrencyPurchaseDto dto)
        {
            return new IncomingCurrencyPurchaseResponse
            {
                Sum = dto.Sum,
                Date = dto.Date,
                Number = dto.Number,
                Description = dto.Description,
                DocumentBaseId = dto.DocumentBaseId,
                SettlementAccountId = dto.SettlementAccountId,
                FromSettlementAccountId = dto.FromSettlementAccountId,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
            };
        }

        public Task CreateAsync(IncomingCurrencyPurchaseDto dto)
        {
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(IncomingCurrencyPurchaseDto dto)
        {
            return apiClient.UpdateAsync($"{path}/{dto.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}