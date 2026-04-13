using System;

namespace Moedelo.Requisites.Enums.TaxationSystems
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
