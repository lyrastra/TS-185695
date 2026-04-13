using System;

namespace Moedelo.PaymentOrderImport.Enums.Attributes
{
    public class AvailableWithAccessRuleAttribute : Attribute
    {
        public AvailableWithAccessRuleAttribute(PaymentImportAccessRule accessRule)
        {
            AccessRule = accessRule;
        }

        public PaymentImportAccessRule AccessRule { get; set; }
    }
}
