using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos.UnrecognizedPayments
{
    public class UnrecognizedMoneyTableResponseDto
    {
        public int TotalCount { get; set; }

        public IReadOnlyCollection<UnrecognizedMoneyTableOperationDto> Operations { get; set; }
    }
}
