using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment
{
    public class EfsEmploymentDto
    {
        public List<EfsEmploymentWorkerDto> Workers { get; set; } = new List<EfsEmploymentWorkerDto>();

        public List<EfsWorkContractDto> WorkContracts { get; set; }

        public List<EfsDismissalWorkerDto> Dismissals { get; set; }

        public List<EfsRecruitmentWorkerDto> Recruitments { get; set; }
    }
}