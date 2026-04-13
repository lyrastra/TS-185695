using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges
{
    public class WorkersWithoutChangesRequest
    {
        public WorkersWithoutChangesRequest(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}
