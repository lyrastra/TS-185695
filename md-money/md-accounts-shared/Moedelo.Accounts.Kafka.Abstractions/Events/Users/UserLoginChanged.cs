namespace Moedelo.Accounts.Kafka.Abstractions.Events.Users
{
    /// <summary>
    /// Событие "У пользователя изменён логин"
    /// </summary>
    public abstract class UserLoginChanged
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// текущий логин (после изменения)
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Старый логин
        /// </summary>
        public string OldLogin { get; set; }
    }
}