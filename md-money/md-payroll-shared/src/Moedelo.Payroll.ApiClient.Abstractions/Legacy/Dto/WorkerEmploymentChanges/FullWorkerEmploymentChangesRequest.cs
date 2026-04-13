using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges
{
    public class FullWorkerEmploymentChangesRequest
    {
        public FullWorkerEmploymentChangesRequest(ICollection<int> workerIds, DateTime beforeDate)
        {
            WorkerIds = workerIds;
            BeforeDate = beforeDate;
        }

        public ICollection<int> WorkerIds { get; set; }

        public DateTime BeforeDate { get; set; }
    }
}
