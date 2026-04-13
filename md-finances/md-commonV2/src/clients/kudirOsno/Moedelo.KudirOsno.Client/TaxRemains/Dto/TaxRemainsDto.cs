using Moedelo.KudirOsno.Client.TaxRemains.Dto;

namespace Moedelo.KudirOsno.Client.Postings.Dto
{
    public class TaxRemainsDto
    {
        public decimal SaleIncomeQuarter1 { get; set; }
        public decimal SaleIncomeQuarter2 { get; set; }
        public decimal SaleIncomeQuarter3 { get; set; }
        public decimal OtherIncomeQuarter1 { get; set; }
        public decimal OtherIncomeQuarter2 { get; set; }
        public decimal OtherIncomeQuarter3 { get; set; }
        public decimal ProductExpensesQuarter1 { get; set; }
        public decimal ProductExpensesQuarter2 { get; set; }
        public decimal ProductExpensesQuarter3 { get; set; }
        public decimal AmortizationExpensesQuarter1 { get; set; }
        public decimal AmortizationExpensesQuarter2 { get; set; }
        public decimal AmortizationExpensesQuarter3 { get; set; }
        public decimal SalaryExpensesQuarter1 { get; set; }
        public decimal SalaryExpensesQuarter2 { get; set; }
        public decimal SalaryExpensesQuarter3 { get; set; }
        public decimal OtherExpensesQuarter1 { get; set; }
        public decimal OtherExpensesQuarter2 { get; set; }
        public decimal OtherExpensesQuarter3 { get; set; }

        /// <summary>
        /// Авансовые платежи
        /// </summary>
        public TaxRemainAdvancePaymentDto[] AdvancePayments { get; set; }
    }
}
