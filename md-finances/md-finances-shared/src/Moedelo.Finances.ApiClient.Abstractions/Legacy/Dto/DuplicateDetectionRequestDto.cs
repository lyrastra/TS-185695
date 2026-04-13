using System.Collections.Generic;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
{
    public class DuplicateDetectionRequestDto
    {
        public int SettlementAccountId { get; set; }
        public IReadOnlyCollection<OperationForDuplicateDetectionDto> Operations { get; set; }
    }
}