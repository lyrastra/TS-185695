using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FundsContributions
{
    public class FundsContributionsDepartmentDto
    {
        public FundsContributionsDepartmentDto()
        {
            Workers = new List<FundsContributionsWorkerDto>();
        }
        
        public string DepartmentName { get; set; }
        
        public List<FundsContributionsWorkerDto> Workers { get; set; }
    }
}
