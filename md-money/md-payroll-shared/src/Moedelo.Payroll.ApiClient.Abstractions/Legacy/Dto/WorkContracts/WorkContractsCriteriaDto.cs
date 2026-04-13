using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkContracts
{
    public class WorkContractsCriteriaDto
    {
        public WorkContractsCriteriaDto()
        {
            FirmIds = new List<int>();
        }

        public IReadOnlyCollection<int> FirmIds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}