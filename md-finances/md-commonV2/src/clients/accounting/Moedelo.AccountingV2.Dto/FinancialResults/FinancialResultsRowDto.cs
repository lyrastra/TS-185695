using Moedelo.Common.Enums.Enums.Accounting;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.FinancialResults
{
    public class FinancialResultsRowDto
    {
        public FinancialResultsRowType Type { get; set; }

        public string Name { get; set; }

        public string Hint { get; set; }

        public string CustomValueHint { get; set; }

        public string CustomValueWatermark { get; set; }

        public bool AllowCustomValues { get; set; }

        public int Sign { get; set; }

        public string Code { get; set; }

        public decimal YearSum { get; set; }

        public decimal PreYearSum { get; set; }

        public bool SelectCustomValuesCode { get; set; }

        public List<FinancialResultsCustomValueDto> CustomValues { get; set; }

        public List<string> Codes { get; set; }
    }
}
