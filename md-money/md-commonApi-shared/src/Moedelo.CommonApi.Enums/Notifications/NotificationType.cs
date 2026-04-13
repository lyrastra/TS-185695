namespace Moedelo.CommonApi.Enums.Notifications
{
    /// <summary>
    /// Тип уведомления
    /// </summary>
    public enum NotificationType
    {
        /// <summary> Новый функционал </summary>
        New = 0,

        /// <summary> Уведомление об изменении </summary>
        Notification = 1,

        /// <summary> Просьба обновить данные или совершить действие </summary>
        Action = 2,

        /// <summary> Информационное </summary>
        Info = 3,

        /// <summary> Опрос </summary>
        Question = 4,

        /// <summary> Создан автодокумент </summary>
        AutoDoc = 5
    }
}
