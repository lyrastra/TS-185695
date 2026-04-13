namespace Moedelo.Accounts.Kafka.Abstractions.Events
{
    /// <summary>
    /// Событие "В аккаунт была добавлена новая фирма"
    /// </summary>
    public abstract class NewFirmWasCreatedInAccountEventFields
    {
        /// <summary>
        /// идентификатор аккаунта
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// идентификатор основной фирмы аккаунта
        /// </summary>
        public int AccountMainFirmId { get; set; }

        /// <summary>
        /// идентификатор созданной фирмы
        /// </summary>
        public int CreatedFirmId { get; set; }
    }
}