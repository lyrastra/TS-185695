namespace Moedelo.Accounts.Kafka.Abstractions.Events
{
    /// <summary>
    /// Событие "Из аккаунта была удалена фирма"
    /// </summary>
    public abstract class FirmWasRemovedFromAccountEventFields
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
        /// идентификатор откреплённой фирмы
        /// </summary>
        public int DeletedFirmId { get; set; }
    }
}