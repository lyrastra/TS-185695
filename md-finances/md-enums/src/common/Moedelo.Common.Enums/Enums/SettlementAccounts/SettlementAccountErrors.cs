namespace Moedelo.Common.Enums.Enums.SettlementAccounts
{
    public enum SettlementAccountCodeErrors
    {
        NoErrors = 0,
        BankNotFound = 101,
        AlreadyExists = 102,
        NotRightsToCreateACurrencyAccount = 103,
        AccountCurrencyNotDefined = 104,
        CurrencyCodeTransitAccountDoesNotMatchCurrent = 105,
        TransitAccountNumberMustNotMatchCurrentAccount = 106,
        TransitAccountWithSameNumberAlreadyExists = 107,
        UnableToChangeSettlementAccountCurrency = 108
    }
}