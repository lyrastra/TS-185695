using System;
using System.Collections.Generic;

namespace Moedelo.RptV2.Dto.DownloadReport
{
    public class DownloadSzvExperienceRequestDto
    {
        public string WorkerName { get; set; }
        
        public string WorkerSurname { get; set; }
        
        public string WorkerPatronymic { get; set; }
        
        public string WorkerSnils { get; set; }
        
        public DateTime? WorkerTerminationDate { get; set; }
        
        public List<DownloadSzvExperiencePeriodRequestDto> Periods { get; set; }
        
        public DateTime ReportDate { get; set; }
    }
}
