using System;
using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.SalaryPayments
{
    public class GetLastDocumentsNumbersRequestDto
    {
        public int Year { get; set; }

        public int? SettlementAccountId { get; set; }

        public IReadOnlyList<int> SalaryProjectSettlementAccountIds { get; set; } = Array.Empty<int>();
    }
}