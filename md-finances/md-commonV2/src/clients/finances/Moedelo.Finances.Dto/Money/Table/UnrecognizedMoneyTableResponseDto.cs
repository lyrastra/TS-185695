using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Dto.Money.Table
{
    public class UnrecognizedMoneyTableResponseDto
    {
        public int TotalCount { get; set; }

        public IReadOnlyCollection<UnrecognizedMoneyTableOperationDto> Operations { get; set; }
    }
}
