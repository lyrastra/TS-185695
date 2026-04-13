using System.Collections.Generic;
using Moedelo.Payroll.Enums.Funds;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FundsContributions
{
    public class FundContributionDto
    {
        public FundContributionDto()
        {
            FundContributions = new List<FundContributionSumDto>();
        }

        public string FundName { get; set; }

        public List<FundContributionSumDto> FundContributions { get; set; }
        
        public FundChargeType FundType { get; set; }
    }
}
