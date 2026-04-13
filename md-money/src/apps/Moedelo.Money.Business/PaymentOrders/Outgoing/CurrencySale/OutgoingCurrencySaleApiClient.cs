using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale
{
    [InjectAsSingleton(typeof(OutgoingCurrencySaleApiClient))]
    internal sealed class OutgoingCurrencySaleApiClient
    {
        private const string path = "Outgoing/CurrencySale";

        private readonly IPaymentOrderApiClient apiClient;

        public OutgoingCurrencySaleApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<OutgoingCurrencySaleResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<OutgoingCurrencySaleDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return MapToResponse(dto);
        }
        
        private static OutgoingCurrencySaleResponse MapToResponse(OutgoingCurrencySaleDto dto)
        {
            return new OutgoingCurrencySaleResponse
            {
                Sum = dto.Sum,
                Date = dto.Date,
                Number = dto.Number,
                Description = dto.Description,
                TotalSum = dto.TotalSum,
                ExchangeRate = dto.ExchangeRate,
                DocumentBaseId = dto.DocumentBaseId,
                ExchangeRateDiff = dto.ExchangeRateDiff,
                ProvideInAccounting = dto.ProvideInAccounting,
                SettlementAccountId = dto.SettlementAccountId,
                ToSettlementAccountId = dto.ToSettlementAccountId,
                TaxPostingsInManualMode = dto.TaxPostingsInManualMode,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId)
            };
        }

        public Task CreateAsync(OutgoingCurrencySaleSaveRequest request)
        {
            var dto = MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(OutgoingCurrencySaleSaveRequest request)
        {
            var dto = MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }
        
        private static OutgoingCurrencySaleDto MapToDto(OutgoingCurrencySaleSaveRequest request)
        {
            return new OutgoingCurrencySaleDto
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
                ProvideInAccounting = request.ProvideInAccounting,
                ToSettlementAccountId = request.ToSettlementAccountId,
                TaxPostingsInManualMode = request.TaxPostings?.ProvidePostingType == ProvidePostingType.ByHand,
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