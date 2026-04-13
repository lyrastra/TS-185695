using Moedelo.AccPostings.Enums;
using System.Collections.Generic;

namespace Moedelo.AccPostings.Kafka.Abstractions.Commands
{
    public class OverwriteAccPostingsV2Command
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Для поддержки старой схемы (из AccountingOperation)
        /// </summary>
        public OperationType OperationType { get; set; }

        public IReadOnlyCollection<AccPostingV2> Postings { get; set; }
    }
}
