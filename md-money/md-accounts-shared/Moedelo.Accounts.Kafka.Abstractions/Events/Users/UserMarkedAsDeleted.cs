namespace Moedelo.Accounts.Kafka.Abstractions.Events.Users
{
    /// <summary>
    /// Событие "Пользователь помечен как удалённый"
    /// </summary>
    public abstract class UserMarkedAsDeleted
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}