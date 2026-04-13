using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto.Postings
{
    public class SalaryPostingForBizDto
    {
        public SalaryPostingForBizDto()
        {
            Funds = new List<FundPostingForBizDto>();    
        }
        
        public int WorkerId { get; set; }

        public ChargePostingForBizDto Charge { get; set; }

        public List<NdflPostingForBizDto> Ndfls { get; set; } = new List<NdflPostingForBizDto>();
        
        public List<FundPostingForBizDto> Funds { get; set; }
    }
}