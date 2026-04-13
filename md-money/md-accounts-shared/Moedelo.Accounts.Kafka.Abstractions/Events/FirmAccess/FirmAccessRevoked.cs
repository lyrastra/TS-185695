namespace Moedelo.Accounts.Kafka.Abstractions.Events.FirmAccess
{
    /// <summary>
    /// Событие "Доступ к фирме отозван"
    /// </summary>
    public abstract class FirmAccessRevoked
    {
        /// <summary>
        /// Id фирмы
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}
