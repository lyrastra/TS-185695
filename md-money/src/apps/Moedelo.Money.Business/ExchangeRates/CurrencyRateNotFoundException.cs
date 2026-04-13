using System;
using Moedelo.Requisites.Enums.SettlementAccounts;

namespace Moedelo.Money.Business.ExchangeRates
{
    internal sealed class CurrencyRateNotFoundException : Exception
    {
        public CurrencyRateNotFoundException(Currency currency, DateTime date) : base($"Not found currency of type {currency} on date {date: dd.MM.yyyy}")
        {
        }
        
        public CurrencyRateNotFoundException(DateTime date) : base($"Not found any currency on date {date: dd.MM.yyyy}")
        {
        }
    }
}