using System;

namespace Moedelo.Requisites.Enums.SettlementAccounts
{
    public class CurrencyRussianNameAttribute : Attribute
    {
        private readonly string russianName;
        
        internal CurrencyRussianNameAttribute(string russianName)
        {
            this.russianName = russianName;
        }

        public override string ToString()
        {
            return russianName;
        }
    }
}