using System;

namespace Moedelo.Parsers.Klto1CParser.Models.BankTo1CParser
{
    public class BudgetDetails
    {
        public string Kbk { get; set; } = string.Empty;
        public string Okato { get; set; } = string.Empty;
        public string BudgetaryPaymentBase { get; set; } = string.Empty;
        public string BudgetaryPeriod { get; set; } = string.Empty;
        public string BudgetaryDocNumber { get; set; } = string.Empty;
        public DateTime BudgetaryDocDate { private get; set; }
        public string BudgetaryPaymentType { get; set; } = string.Empty;
        public string BudgetaryPayerStatus { get; set; } = string.Empty;

        public string GetBudgetaryDocDate()
        {
            return BudgetaryDocDate == DateTime.MinValue ? string.Empty : BudgetaryDocDate.ToString("dd.MM.yyyy");
        }
    }
}