namespace Moedelo.Common.Enums.Enums.Calendar
{
    /// <summary>
    /// Тип ссылки в кастомном событии из партнёрки
    /// </summary>
    public enum UrlType
    {
        /// <summary> Без Url </summary>
        None = 0,

        /// <summary> Вручную указанный Url </summary>
        Manual = 1,

        /// <summary> Url на отправку сообщения в CRM </summary>
        Crm = 2,
    }
}