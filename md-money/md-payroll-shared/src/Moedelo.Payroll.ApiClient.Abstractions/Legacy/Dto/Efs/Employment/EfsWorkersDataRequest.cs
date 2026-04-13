using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment
{
    public class EfsWorkersDataRequest
    {
        public List<int> WorkerIds { get; set; }

        public DateTime Date { get; set; }
    }
}