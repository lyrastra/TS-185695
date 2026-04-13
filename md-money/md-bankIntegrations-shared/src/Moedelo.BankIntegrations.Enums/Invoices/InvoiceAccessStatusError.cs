namespace Moedelo.BankIntegrations.Enums.Invoices
{
    public enum InvoiceAccessStatusError
    {
        None = 0,
        BankAccessError = 1,
        SberOfferError = 2,
        SberInvalidSettlementAccount = 3
    }
}
