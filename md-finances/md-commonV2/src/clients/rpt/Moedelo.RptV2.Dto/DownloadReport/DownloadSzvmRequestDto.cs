using System;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.RptV2.Dto.DownloadReport
{
    public class DownloadSzvmRequestDto
    {
        public string WorkerName { get; set; }

        public string WorkerSurname { get; set; }

        public string WorkerPatronymic { get; set; }
        
        public string WorkerSnils { get; set; }
        
        public string WorkerInn { get; set; }
        
        public DateTime ReportDate { get; set; }
    }
}
