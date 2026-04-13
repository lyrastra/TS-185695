namespace Moedelo.ErptV2.Dto.NotSendedReports
{
    public class GetNotSentReportsRequestDto
    {
        public PeriodFilterDto PeriodFilter { get; set; }

        public ReportType[] ReportTypeFilter { get; set; }

        public FirmReportTypeFilterDto[] FirmReportTypeFilter { get; set; }

        public SortType SortType { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}
