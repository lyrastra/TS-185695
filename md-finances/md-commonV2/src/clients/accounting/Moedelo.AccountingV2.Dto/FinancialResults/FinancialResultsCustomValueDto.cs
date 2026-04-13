namespace Moedelo.AccountingV2.Dto.FinancialResults
{
    public class FinancialResultsCustomValueDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public decimal YearSum { get; set; }

        public decimal PreYearSum { get; set; }
    }
}
