namespace Moedelo.Accounts.Kafka.Abstractions.Events.Firm
{
    /// <summary>
    /// Событие "смена флага IsInternal (не учитывать в статистике)"
    /// </summary>
    public abstract class FirmIsInternalChanged
    {
        /// <summary>
        /// Id фирмы
        /// </summary>
        public int FirmId { get; set; }
        /// <summary>
        /// Флаг "не учитывать в статистике"
        /// </summary>
        public bool IsInternal { get; set; }
    }
}