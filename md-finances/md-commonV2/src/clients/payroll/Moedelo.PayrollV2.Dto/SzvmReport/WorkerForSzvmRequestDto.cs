using System;
using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto.SzvmReport
{
    public class WorkerForSzvmRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IReadOnlyCollection<int>  WorkerIds { get; set; }
    }
}