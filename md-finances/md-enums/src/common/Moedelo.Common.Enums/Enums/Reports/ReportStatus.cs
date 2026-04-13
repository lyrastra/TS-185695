namespace Moedelo.Common.Enums.Enums.Reports
{
    /// <summary>
    /// Статус отчета
    /// </summary>
    public enum ReportStatus
    {
        /// <summary>
        /// Не заходили
        /// </summary>
        New = 0,

        /// <summary>
        /// В процессе
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Не отправлен
        /// </summary>
        NotSended = 2,

        /// <summary>
        /// На обработке
        /// </summary>
        OnProcessing = 3,

        /// <summary>
        /// Отклонен
        /// </summary>
        Rejected = 4,

        /// <summary>
        /// Принят
        /// </summary>
        Accepted = 5,

        /// <summary>
        /// Завершен
        /// </summary>
        Completed = 6,

        /// <summary>
        /// Завершен по стрелке
        /// </summary>
        CompletedByArrow = 7
    }
}