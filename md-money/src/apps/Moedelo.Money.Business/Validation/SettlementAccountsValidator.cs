using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.SettlementAccounts;
using System.Threading.Tasks;
using Moedelo.Requisites.Enums.SettlementAccounts;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(ISettlementAccountsValidator))]
    internal sealed class SettlementAccountsValidator : ISettlementAccountsValidator
    {
        private readonly ISettlementAccountsReader settlementAccountsReader;

        public SettlementAccountsValidator(ISettlementAccountsReader settlementAccountsReader)
        {
            this.settlementAccountsReader = settlementAccountsReader;
        }

        public async Task ValidateAsync(int settlementAccountId)
        {
            var settlementAccount = await settlementAccountsReader.GetByIdAsync(settlementAccountId);
            if (settlementAccount == null)
            {
                throw new BusinessValidationException("SettlementAccountId", $"Не найден рассчетный счет с идентификатором {settlementAccountId}");
            }
            if (settlementAccount.SubcontoId == null)
            {
                throw new BusinessValidationException("SettlementAccountId", $"Отсутствует субконто рассчетного счета с идентификатором {settlementAccountId}");
            }
        }

        public async Task<SettlementAccount> ValidateCurrencyAsync(int settlementAccountId)
        {
            var currency = await settlementAccountsReader.GetByIdAsync(settlementAccountId);

            ValidateExistence(settlementAccountId, currency);
            ValidateCurrency(currency);

            return currency;
        }

        public async Task<(SettlementAccount rub, SettlementAccount currency)> ValidateRubAndCurrencyAsync(
            int rubSettlementAccountId,
            int currencySettlementAccountId,
            bool forSameBank = true)
        {
            var rub = await settlementAccountsReader.GetByIdAsync(rubSettlementAccountId);
            var currency = await settlementAccountsReader.GetByIdAsync(currencySettlementAccountId);

            ValidateExistence(rubSettlementAccountId, rub);
            ValidateExistence(currencySettlementAccountId, currency);
            
            ValidateRub(rub);
            ValidateCurrency(currency);

            if (forSameBank)
            {
                ValidateForSameBank(rub, currency);                
            }

            return (rub, currency);
        }
        
        public async Task ValidateCurrencyAccountsOfSameCurrencyTypeAsync(int firstAccountId, int secondAccountId)
        {
            var first = await settlementAccountsReader.GetByIdAsync(firstAccountId);
            var second = await settlementAccountsReader.GetByIdAsync(secondAccountId);

            ValidateExistence(firstAccountId, first);
            ValidateExistence(secondAccountId, second);
            
            ValidateCurrency(first);
            ValidateCurrency(second);
            
            ValidateForSameCurrency(first, second);
        }

        private void ValidateForSameCurrency(SettlementAccount first, SettlementAccount second)
        {
            if (first.Currency != second.Currency)
            {
                throw new BusinessValidationException(nameof(first.Type), $"Cчета с идентификаторами {first.Id} и {second.Id} должны быть в одной валюте");
            }
        }

        private void ValidateRub(SettlementAccount settlementAccount)
        {
            if (settlementAccount.Currency != Currency.RUB || settlementAccount.Type != SettlementAccountType.Default)
            {
                throw new BusinessValidationException(nameof(settlementAccount.Type), $"Cчёт с идентификатором {settlementAccount.Id} не является рублёвым");
            }
        }

        private void ValidateCurrency(SettlementAccount settlementAccount)
        {
            if (settlementAccount.Currency == Currency.RUB || settlementAccount.Type == SettlementAccountType.Default)
            {
                throw new BusinessValidationException(nameof(settlementAccount.Type), $"Cчёт с идентификатором {settlementAccount.Id} не является валютным");
            }
        }

        private static void ValidateForSameBank(SettlementAccount settlementAccount, SettlementAccount secondarySettlementAccount)
        {
            if (settlementAccount.BankId != secondarySettlementAccount.BankId)
            {
                throw new BusinessValidationException(nameof(settlementAccount.BankId),
                    $"Расчётный счёт c идентификаторами {settlementAccount.Id} и {secondarySettlementAccount.Id} должны быть в одном банке");
            }
        }

        private static void ValidateExistence(int settlementAccountId, SettlementAccount settlementAccount)
        {
            if (settlementAccount == null)
            {
                throw new BusinessValidationException(nameof(settlementAccountId), $"Не найден рассчетный счет с идентификатором {settlementAccountId}");
            }
        }
    }
}
