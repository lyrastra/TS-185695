using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FundsContributions
{
    public class FundsContributionsDto
    {
        public FundsContributionsDto()
        {
            Departments = new List<FundsContributionsDepartmentDto>();
        }
        
        public List<FundsContributionsDepartmentDto> Departments { get; set; }

        public decimal YearIncome => Departments.SelectMany(x => x.Workers).Sum(x => x.YearIncomeSum);
        
        public decimal MonthIncome => Departments.SelectMany(x => x.Workers).Sum(x => x.MonthIncomeSum);

        public decimal PfrSum => Departments.SelectMany(x => x.Workers).Sum(x => x.PfrSum);

        public decimal FomsSum => Departments.SelectMany(x => x.Workers).Sum(x => x.FomsSum);

        public decimal DisabilityFssSum => Departments.SelectMany(x => x.Workers).Sum(x => x.DisabilityFssSum);

        public decimal InjuredFssSum => Departments.SelectMany(x => x.Workers).Sum(x => x.InjuredFssSum);

        public decimal SfrSum => Departments.SelectMany(x => x.Workers).Sum(x => x.SfrSum);
    }
}
