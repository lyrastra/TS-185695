using System;

namespace Moedelo.Requisites.Enums.SettlementAccounts
{
    public class CurrencySymbolAttribute : Attribute
    {
        private readonly string symbol;
        
        internal CurrencySymbolAttribute(string symbol)
        {
            this.symbol = symbol;
        }

        public override string ToString()
        {
            return symbol;
        }
    }
}