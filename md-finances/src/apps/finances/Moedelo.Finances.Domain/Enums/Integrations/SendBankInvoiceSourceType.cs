namespace Moedelo.Finances.Domain.Enums.Integrations
{
    /// <summary> Тип источника отправки сквозного(прямого) платежа</summary>
    public enum SendBankInvoiceSourceType
    {
        /// <summary> Отправка через веб-интерфейс </summary>
        Web,
        /// <summary> Отправка через мобильное приложение </summary>
        Mobile
    }
}
