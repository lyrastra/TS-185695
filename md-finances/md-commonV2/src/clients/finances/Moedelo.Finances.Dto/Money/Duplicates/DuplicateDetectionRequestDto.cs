using System.Collections.Generic;

namespace Moedelo.Finances.Dto.Money.Duplicates
{
    public class DuplicateDetectionRequestDto
    {
        public int SettlementAccountId { get; set; }
        public IReadOnlyCollection<OperationForDuplicateDetectionDto> Operations { get; set; }
    }
}