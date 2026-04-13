using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class WorkerForInsuredFssAutocompleteRequestDto
    {
        public string Query { get; set; }

        public int Count { get; set; }

        public IReadOnlyCollection<int> ExcludeWorkerIds { get; set; } = Array.Empty<int>();
    }
}