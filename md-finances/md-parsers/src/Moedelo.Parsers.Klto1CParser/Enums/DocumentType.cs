using System;

namespace Moedelo.Parsers.Klto1CParser.Enums
{
    public enum SectionDocumentType
    {
        PaymentRequest,
        BankOrder,
        MemorialOrder
    }

    public static class SectionDocuments
    {
        public static string GetSectionDocumentName(this SectionDocumentType type)
        {
            switch (type)
            {
                case SectionDocumentType.PaymentRequest:
                    return "Платежное поручение";
                case SectionDocumentType.BankOrder:
                    return "Банковский ордер";
                case SectionDocumentType.MemorialOrder:
                    return "Мемориальный ордер";
                default:
                    throw new NotImplementedException(nameof(SectionDocumentType));
            }
        }
    }
}
