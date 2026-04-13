namespace Moedelo.Common.Enums.Enums.RegistrationService
{
    /// <summary>
    /// Статусы регистрации
    /// </summary>
    public enum RegistrationStatus
    {
        /// <summary>
        /// Неопределено
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Первичная
        /// </summary>
        Primary = 1,

        /// <summary>
        /// Реактивация
        /// </summary>
        Reactivation = 2,

        /// <summary>
        /// Повторная
        /// </summary>
        Repeated = 3,

        /// <summary>
        /// Попытка реактивации
        /// </summary>
        AttemptReactivation = 4,

        /// <summary>
        /// Повторная попытка
        /// </summary>
        AttemptRepeated = 5,
    }
}