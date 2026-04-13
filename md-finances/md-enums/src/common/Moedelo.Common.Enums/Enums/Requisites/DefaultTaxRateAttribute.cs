using System;

namespace Moedelo.Common.Enums.Enums.Requisites
{
    public class DefaultTaxRateAttribute : Attribute
    {
        public DefaultTaxRateAttribute(double taxRate)
        {
            TaxRate = taxRate;
        }

        public double TaxRate { get; set; }
    }
}