using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.NotSendedReports
{
    public class FirmReportTypeFilterDto
    {
        public int Id { get; set; }

        public List<ReportType> ReportTypes { get; set; }
    }
}