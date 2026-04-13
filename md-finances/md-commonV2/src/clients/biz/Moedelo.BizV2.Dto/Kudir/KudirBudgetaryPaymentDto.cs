using System;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.BizV2.Dto.Kudir
{
    public class KudirBudgetaryPaymentDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public BudgetaryPaymentType Type { get; set; }
        public string Description { get; set; }
        public int PeriodYear { get; set; }
    }
}
