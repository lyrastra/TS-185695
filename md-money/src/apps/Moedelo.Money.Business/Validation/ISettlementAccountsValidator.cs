using System.Threading.Tasks;
using Moedelo.Money.Business.SettlementAccounts;

namespace Moedelo.Money.Business.Validation
{
    internal interface ISettlementAccountsValidator
    {
        Task ValidateAsync(int settlementAccountId);
        
        /// <summary>
        /// Валидация валютных р/сч (возвращает р/сч, если он существует) 
        /// </summary>
        Task<SettlementAccount> ValidateCurrencyAsync(int settlementAccountId);

        Task<(SettlementAccount rub, SettlementAccount currency)> ValidateRubAndCurrencyAsync(
            int rubSettlementAccountId, int currencySettlementAccountId, bool forSameBank = true);

        /// <summary>
        /// Валидация валютных р/сч в одной валюте 
        /// </summary>
        Task ValidateCurrencyAccountsOfSameCurrencyTypeAsync(int firstAccountId, int secondAccountId);
    }
}
