namespace Moedelo.BankIntegrations.Enums.Sberbank
{
    public enum PaymentRequestCategory
    {
        AccountLimit = 0,
        ToCardFile,
        InvalidTokens,
        NoAccounts,
        AccountBlocked,
        AccountClosed,
        Other = 999
    }
}
