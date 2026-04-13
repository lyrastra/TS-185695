namespace Moedelo.TaxSelfCost.Dto.FiFo.Enums
{
    /// <summary>
    /// Представляет перечисление статусов генерации проводок.
    /// </summary>
    public enum GenerationStatusEnum
    {
        /// <summary>
        /// Генерация проводок не запущена.
        /// </summary>
        NotStarted = 0,

        /// <summary>
        /// Генерация проводок в процессе.
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Генерация проводок завершена.
        /// </summary>
        Completed = 2
    }
}
