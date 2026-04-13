using System;
using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.PayrollV2.Dto.Postings
{
    public class FundPostingForBizDto
    {
        public FundChargeType FundType { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
    }
}