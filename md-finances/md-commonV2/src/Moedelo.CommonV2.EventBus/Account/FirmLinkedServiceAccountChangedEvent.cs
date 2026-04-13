using System;
using Moedelo.Common.Enums.Enums.Account;

namespace Moedelo.CommonV2.EventBus.Account
{
    /// <summary>
    /// Событие: у фирмы изменился обслуживающий профаутсорсер
    /// </summary>
    public class FirmLinkedServiceAccountChangedEvent
    {
        public DateTime Timestamp { get; set; }
        public int FirmId { get; set; }
        public int? PreviousAccountId { get; set; }
        public int? CurrentAccountId { get; set; }
        /// <summary>
        /// автор изменений
        /// </summary>
        public int AuthorUserId { get; set; }
    }
}