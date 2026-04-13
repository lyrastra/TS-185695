namespace Moedelo.BankIntegrations.Enums.Invoices
{
    public enum InvoiceStatus
    {
        /// <summary> Неизвестен </summary>
        Unknown = -1, 
        /// <summary> Ожидает оплату </summary>
        Pending = 0,
        /// <summary> Исполнен </summary>
        Processed,
        /// <summary> Ошибка на стороне банка </summary>
        Failed,
        /// <summary> Отменён </summary>
        Canceled
    }
}