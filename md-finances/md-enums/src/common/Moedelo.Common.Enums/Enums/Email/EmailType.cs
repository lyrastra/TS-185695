namespace Moedelo.Common.Enums.Enums.Email
{
    /// <summary>
    /// Тип Email
    /// </summary>
    public enum EmailType : byte
    {
        /// <summary>
        /// Email с данным типом содержит адрес лица, принимающего решение об оплате сервиса при автоматическом выставлении счетов
        /// </summary>
        AutoCreateBill = 0,

        /// <summary>
        /// Email, который определён почтовым сервисом, как несуществующий
        /// </summary>
        NonExistent = 1,

        /// <summary>
        /// Email заполняется для получения закакза по прайс листу, в случае, если он отличается от логина
        /// </summary>
        StockVendibles = 2
    }
}