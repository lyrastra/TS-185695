namespace Moedelo.CommonApi.Enums.Notifications
{
    /// <summary>
    /// Тип получателей уведомлений
    /// </summary>
    public enum NotificationRecipientType
    {
        /// <summary>
        /// Все пользователи
        /// </summary>
        All = 0,

        /// <summary>
        /// Пользователи БИЗ
        /// </summary>
        Biz = 1,

        /// <summary>
        /// Пользователи Учетки
        /// </summary>
        Accounting = 2,

        /// <summary>
        /// Пользователи Бюро
        /// </summary>
        Buro = 3,

        /// <summary>
        /// Пользователи из списка
        /// </summary>
        List = 4
    }
}
