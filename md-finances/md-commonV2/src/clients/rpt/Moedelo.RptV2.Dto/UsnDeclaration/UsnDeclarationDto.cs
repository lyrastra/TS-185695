namespace Moedelo.RptV2.Dto.UsnDeclaration
{
    public class UsnDeclarationDto
    {
        public int Year { get; set; }
        public int PeriodNumber { get; set; }
        public decimal TaxSumForPayment { get; set; }
        public decimal TaxPostingSumQuarter { get; set; }
        public decimal TaxPostingSumHalfYear { get; set; }
        public decimal TaxPostingSumNineMonth { get; set; }
        public decimal TaxPostingSumYear { get; set; }
        public decimal MinimalTaxDifference { get; set; }
        public decimal ReduceIncomeByLosses { get; set; }

        /// <summary>
        /// Усть убытки
        /// </summary>
        public bool HasLosses { get; set; }
    }
}
