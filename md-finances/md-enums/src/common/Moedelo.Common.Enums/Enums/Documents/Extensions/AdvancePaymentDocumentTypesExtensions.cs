namespace Moedelo.Common.Enums.Enums.Documents.Extensions
{
    public static class AdvancePaymentDocumentTypesExtensions
    {
        public static AccountingDocumentType ToAccountingDocumentType(this AdvancePaymentDocumentTypes type)
        {
            switch (type)
            {
                case AdvancePaymentDocumentTypes.CashOrder:
                    return AccountingDocumentType.OutcomingCashOrder;
                case AdvancePaymentDocumentTypes.PaymentOrder:
                    return AccountingDocumentType.PaymentOrder;
                case AdvancePaymentDocumentTypes.Advance:
                    return AccountingDocumentType.AccountingAdvanceStatement;
                default:
                    return AccountingDocumentType.Default;
            }
        }
    }
}