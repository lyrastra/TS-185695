using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class CurrencyPurchaseAndSaleOutgoingOperationDto : OutgoingOperationDto
    {
        public decimal ExchangeRate { get; set; }
        public int? SettlementAccountFromId { get; set; }
        public int? SettlementAccountToId { get; set; }

        public override string Name => FinancialOperationNames.CurrencyPurchaseAndSaleOutgoingOperation;
    }
}