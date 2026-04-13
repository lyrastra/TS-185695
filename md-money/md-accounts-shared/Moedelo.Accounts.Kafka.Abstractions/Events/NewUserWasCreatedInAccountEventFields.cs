namespace Moedelo.Accounts.Kafka.Abstractions.Events
{
    /// <summary>
    /// Событие "В аккаунт был добавлен новый пользователь"
    /// </summary>
    public abstract class NewUserWasCreatedInAccountEventFields
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
        /// идентификатор созданного пользователя
        /// </summary>
        public int CreatedUserId { get; set; }
    }
}