namespace Moedelo.BankIntegrations.Enums.Invoices
{
    public enum InvoiceResponseStatusCode
    {
        Ok = 0,
        /// <summary> Если не хватает прав у клиента для выполнения запроса в банке </summary>
        AccessError,
        /// <summary> Если реквизиты в банке не прошли валидацию </summary>
        RequisiteError,
        /// <summary> Если по счету нельзя выполнять сквозной платеж </summary>
        InvalidSettlementAccount,
        /// <summary> Если платеж не прошёл валидацию </summary>
        ValidationError
    }
}
