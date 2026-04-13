using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class WorkerListRequest
    {
        public WorkerListRequest(IReadOnlyCollection<int> workerIds, bool excludeChildCareOver3Years)
        {
            WorkerIds = workerIds;
            ExcludeChildCareOver3Years = excludeChildCareOver3Years;
        }

        public WorkerListRequest(IReadOnlyCollection<int> workerIds)
        {
            WorkerIds = workerIds;
        }

        public IReadOnlyCollection<int> WorkerIds { get; }
        public bool ExcludeChildCareOver3Years { get; set; }
    }
}