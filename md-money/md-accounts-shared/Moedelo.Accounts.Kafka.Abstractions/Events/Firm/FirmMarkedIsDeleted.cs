namespace Moedelo.Accounts.Kafka.Abstractions.Events.Firm
{
    /// <summary>
    /// Событие удаление аккаунта из реквизитов
    /// Физического удаления не происходит, только проставляется флаг is_deleted у фирмы и у пользователя
    /// </summary>
    public class FirmMarkedIsDeleted
    {
        /// <summary>
        /// Id фирмы
        /// </summary>
        public int FirmId { get; set; }
    }
}