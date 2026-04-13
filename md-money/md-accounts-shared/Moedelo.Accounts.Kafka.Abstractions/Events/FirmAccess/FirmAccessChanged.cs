namespace Moedelo.Accounts.Kafka.Abstractions.Events.FirmAccess
{
    /// <summary>
    /// Событие "Изменение доступа к фирме"
    /// </summary>
    public abstract class FirmAccessChanged
    {
        /// <summary>
        /// Id фирмы
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Признак: главный пользователь
        /// </summary>
        public bool IsLegal { get; set; }

        /// <summary>
        /// Id роли
        /// </summary>
        public int? RoleId { get; set; }
    }
}
