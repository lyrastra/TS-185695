namespace Moedelo.Common.Enums.Enums.EdsRequestTasks
{
    /// <summary>
    /// Статус задачи в партнерке на запрос ЭП
    /// </summary>
    public enum EdsRequestTaskStatus
    {
        /// <summary>
        /// Новый запрос
        /// </summary>
        New = 0,
        
        /// <summary>
        /// Обработанный запрос
        /// </summary>
        Processed = 1,
        
        /// <summary>
        /// Запрос, утративший свою актуальность прежде, чем его обработали
        /// </summary>
        Outdated = 2
    }
}