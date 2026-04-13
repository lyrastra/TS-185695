using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Maintenance
{
    /// <summary>
    /// Событие "Фирмы были удалены"
    /// </summary>
    public class FirmsDeletedEvent
    {
        /// <summary>
        /// Список идентификаторов удалённых фирм
        /// </summary>
        public IReadOnlyCollection<int> FirmIds { get; set; }
    }
}