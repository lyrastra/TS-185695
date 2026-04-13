using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase
{
    [InjectAsSingleton(typeof(OutgoingCurrencyPurchaseApiClient))]
    internal sealed class OutgoingCurrencyPurchaseApiClient
    {
        private const string path = "Outgoing/CurrencyPurchase";

        private readonly IPaymentOrderApiClient apiClient;

        public OutgoingCurrencyPurchaseApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<OutgoingCurrencyPurchaseResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<OutgoingCurrencyPurchaseDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return MapToResponse(dto);
        }

        private static OutgoingCurrencyPurchaseResponse MapToResponse(OutgoingCurrencyPurchaseDto dto)
        {
            return new OutgoingCurrencyPurchaseResponse
            {
                Sum = dto.Sum,
                Date = dto.Date,
                Number = dto.Number,
                Description = dto.Description,
                TotalSum = dto.TotalSum,
                ExchangeRate = dto.ExchangeRate,
                DocumentBaseId = dto.DocumentBaseId,
                ExchangeRateDiff = dto.ExchangeRateDiff,
                SettlementAccountId = dto.SettlementAccountId,
                ToSettlementAccountId = dto.ToSettlementAccountId,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId)
            };
        }

        public Task CreateAsync(OutgoingCurrencyPurchaseSaveRequest request)
        {
            var dto = MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(OutgoingCurrencyPurchaseSaveRequest request)
        {
            var dto = MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        private static OutgoingCurrencyPurchaseDto MapToDto(OutgoingCurrencyPurchaseSaveRequest request)
        {
            return new OutgoingCurrencyPurchaseDto
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                TotalSum = request.TotalSum,
                ExchangeRate = request.ExchangeRate,
                DocumentBaseId = request.DocumentBaseId,
                ExchangeRateDiff = request.ExchangeRateDiff,
                SettlementAccountId = request.SettlementAccountId,
                ToSettlementAccountId = request.ToSettlementAccountId,
                ProvideInAccounting = request.ProvideInAccounting,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                SourceFileId = request.SourceFileId,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}