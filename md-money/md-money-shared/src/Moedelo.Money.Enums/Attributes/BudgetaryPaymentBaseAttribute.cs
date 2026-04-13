using System;

namespace Moedelo.Money.Enums.Attributes
{
    public class BudgetaryPaymentBaseAttribute : Attribute
    {
        private readonly string[] detectStrings;

        public string[] DetectStrings => detectStrings;

        public BudgetaryPaymentBaseAttribute() : this(string.Empty)
        {
        }

        public BudgetaryPaymentBaseAttribute(params string[] detectStrings)
        {
            this.detectStrings = detectStrings;
        }
    }
}