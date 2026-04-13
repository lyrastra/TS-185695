namespace Moedelo.Accounts.Kafka.Abstractions.Events.Users
{
    /// <summary>
    /// Событие "у пользователя изменилось ФИО"
    /// </summary>
    public abstract class UserFioChanged
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// ФИО (после изменения)
        /// </summary>
        public string Fio { get; set; }
    }
}
