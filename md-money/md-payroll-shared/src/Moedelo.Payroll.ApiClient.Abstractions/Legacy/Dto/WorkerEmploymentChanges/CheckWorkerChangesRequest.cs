using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges
{
    public class CheckWorkerChangesRequest
    {
        public CheckWorkerChangesRequest(IReadOnlyCollection<int> workerIds, DateTime startDate, DateTime endDate)
        {
            WorkerIds = workerIds;
            StartDate = startDate;
            EndDate = endDate;
        }

        public IReadOnlyCollection<int> WorkerIds { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}
