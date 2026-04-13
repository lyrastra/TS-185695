namespace Moedelo.BankIntegrations.Enums.Invoices
{
    public enum InvoiceAccessStatus
    {
        /// <summary> Опубликовано </summary>
        Published,

        /// <summary> Ожидает авторизации </summary>
        Awaiting,

        /// <summary> Отклонено клиентом </summary>
        Rejected,

        /// <summary> Отозвано банком </summary>
        Revoked
    }
}