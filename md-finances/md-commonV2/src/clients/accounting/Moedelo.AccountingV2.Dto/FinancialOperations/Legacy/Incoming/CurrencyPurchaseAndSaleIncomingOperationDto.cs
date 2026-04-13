using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class CurrencyPurchaseAndSaleIncomingOperationDto : IncomingOperationDto
    {
        public override string Name => FinancialOperationNames.CurrencyPurchaseAndSaleIncomingOperation;
    }
}