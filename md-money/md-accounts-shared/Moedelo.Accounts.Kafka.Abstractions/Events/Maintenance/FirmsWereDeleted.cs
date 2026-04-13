using System.Collections.Generic;

namespace Moedelo.Accounts.Kafka.Abstractions.Events.Maintenance
{
    /// <summary>
    /// Событие "Фирмы были удалены"
    /// </summary>
    public abstract class FirmsWereDeleted
    {
        /// <summary>
        /// Список идентификаторов фирм, которые были удалены
        /// </summary>
        public IReadOnlyCollection<int> FirmIds { get; set; }
    }
}