namespace Moedelo.Common.Enums.Enums.Tasks
{
    public enum BackgroundTaskStatus
    {
        Default = 0,

        /// <summary>
        /// Режим ожидания
        /// </summary>
        Idle = 1,

        /// <summary>
        /// В обработке
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// Завершен успешно
        /// </summary>
        Completed = 3,

        /// <summary>
        /// Ошибка при обработке
        /// </summary>
        Failed = 4
    }
}