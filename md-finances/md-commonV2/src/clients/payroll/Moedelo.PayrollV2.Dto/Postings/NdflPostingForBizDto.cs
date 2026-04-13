using System;

namespace Moedelo.PayrollV2.Dto.Postings
{
    public class NdflPostingForBizDto
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
        
        public bool IsNdflRate15OverLimit { get; set; }
    }
}