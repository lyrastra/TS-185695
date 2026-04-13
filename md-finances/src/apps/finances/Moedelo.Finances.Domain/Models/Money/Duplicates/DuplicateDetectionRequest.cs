using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Duplicates
{
    public class DuplicateDetectionRequest
    {
        public int FirmId { get; set; }
        public bool IsAccounting { get; set; }
        public int SettlementAccountId { get; set; }
        public IReadOnlyCollection<OperationForDuplicateDetection> Operations { get; set; }
    }
}