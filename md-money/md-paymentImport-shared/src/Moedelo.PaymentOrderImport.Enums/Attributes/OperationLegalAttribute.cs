using System;

namespace Moedelo.PaymentOrderImport.Enums.Attributes
{
    public class OperationLegalAttribute : Attribute
    {
        public OperationLegalAttribute(LegalType legalType)
        {
            LegalType = legalType;
        }

        public LegalType LegalType { get; set; }
    }
}
