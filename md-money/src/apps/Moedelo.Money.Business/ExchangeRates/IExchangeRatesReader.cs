using Moedelo.Requisites.Enums.SettlementAccounts;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.ExchangeRates
{
    public interface IExchangeRatesReader
    {
        Task<decimal> GetByDateAndCurrencyAsync(DateTime date, Currency currency);
        
        Task<bool> ExistsRateAsync(DateTime date, Currency currency);
        
        Task<decimal?> GetByDateAndSettlementAccountIdAsync(DateTime date, int settlementAccountId);
    }
}