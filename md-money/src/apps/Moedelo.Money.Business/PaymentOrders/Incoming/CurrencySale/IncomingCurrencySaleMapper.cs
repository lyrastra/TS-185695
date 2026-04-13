using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale
{
    internal static class IncomingCurrencySaleMapper
    {
        internal static IncomingCurrencySaleSaveRequest MapToSaveRequest(IncomingCurrencySaleResponse response)
        {
            return new IncomingCurrencySaleSaveRequest
            {
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                Description = response.Description,
                DocumentBaseId = response.DocumentBaseId,
                SettlementAccountId = response.SettlementAccountId,
                FromSettlementAccountId = response.FromSettlementAccountId,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                ProvideInAccounting = response.ProvideInAccounting,
            };
        }

        internal static IncomingCurrencySaleSaveRequest MapToSaveRequest(IncomingCurrencySaleImportRequest request)
        {
            return new IncomingCurrencySaleSaveRequest
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                SettlementAccountId = request.SettlementAccountId,
                FromSettlementAccountId = request.FromSettlementAccountId,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                ProvideInAccounting = true,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }
    }
}
