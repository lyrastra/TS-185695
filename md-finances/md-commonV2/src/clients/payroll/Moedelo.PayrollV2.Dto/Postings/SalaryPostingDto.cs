using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.PayrollV2.Dto.Postings
{
    public class SalaryPostingDto
    {
        public int DivisionId { get; set; }

        public string DivisionName { get; set; }

        public string WorkerName { get; set; }

        public int WorkerId { get; set; }

        public string Description { get; set; }

        public decimal Sum { get; set; }
        
        public SyntheticAccountCode DebitCode { get; set; }

        public SyntheticAccountCode CreditCode { get; set; }

        public List<SalarySubcontoDto> DebitSubcontos { get; set; }

        public List<SalarySubcontoDto> CreditSubcontos { get; set; }

        public DateTime ChargeDate { get; set; } 
    }
}