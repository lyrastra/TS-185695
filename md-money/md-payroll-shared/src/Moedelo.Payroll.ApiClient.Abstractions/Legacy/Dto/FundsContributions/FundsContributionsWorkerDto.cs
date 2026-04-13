using System.Collections.Generic;
using System.Linq;
using Moedelo.Payroll.Enums.Funds;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FundsContributions
{
    public class FundsContributionsWorkerDto
    {
        public FundsContributionsWorkerDto()
        {
            FundsContributions = new List<FundContributionDto>();
        }

        public int WorkerId { get; set; }
        
        public string Fio { get; set; }
        
        public string TableNumber { get; set; }
        
        public decimal YearIncomeSum { get; set; }
        
        public decimal MonthIncomeSum { get; set; }
        
        public List<FundContributionDto> FundsContributions { get; set; }
        
        public decimal PfrSum => FundsContributions.Where(f => f.FundType == FundChargeType.InsurancePfr)
                .SelectMany(t => t.FundContributions).Sum(x => x.Sum);

        public decimal FomsSum => FundsContributions.Where(f => f.FundType == FundChargeType.FederalFoms)
                .SelectMany(t => t.FundContributions).Sum(x => x.Sum);

        public decimal DisabilityFssSum => FundsContributions.Where(f => f.FundType == FundChargeType.DisabilityFss)
                .SelectMany(t => t.FundContributions).Sum(x => x.Sum);

        public decimal InjuredFssSum => FundsContributions.Where(f => f.FundType == FundChargeType.InjuredFss)
                .SelectMany(t => t.FundContributions).Sum(x => x.Sum);

        public decimal SfrSum => FundsContributions.Where(f => f.FundType == FundChargeType.Sfr)
            .SelectMany(t => t.FundContributions).Sum(x => x.Sum);
    }
}
