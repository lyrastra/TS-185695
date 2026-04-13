namespace Moedelo.BankIntegrations.Enums.Invoices
{
    /// <summary> Тип источника создания сквозного(прямого) платежа</summary>
    public enum InvoiceSource
    {
        /// <summary> Создано из веб-интерфейса </summary>
        Ib = 0,
        /// <summary> Создано из мобильного приложения </summary>
        Mobile
    }
}