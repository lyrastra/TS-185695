using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerPositions
{
    public class WorkerPositionOnFireDateDto
    {
        public int WorkerId { get; set; }

        public string Position { get; set; }

        public DateTime? EndDate { get; set; }
        
        public DateTime? StartDate { get; set; }
    }
}
