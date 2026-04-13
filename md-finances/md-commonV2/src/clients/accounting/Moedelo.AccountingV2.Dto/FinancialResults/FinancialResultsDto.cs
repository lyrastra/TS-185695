using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.FinancialResults
{
    public class FinancialResultsDto
    {
        public List<FinancialResultsRowDto> Rows { get; set; }

        public decimal YearSum { get; set; }

        public decimal PreYearSum { get; set; }

        public bool IsSaved { get; set; }
    }
}
