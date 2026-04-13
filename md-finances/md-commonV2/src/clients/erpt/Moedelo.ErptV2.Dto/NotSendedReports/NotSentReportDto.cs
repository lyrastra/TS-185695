namespace Moedelo.ErptV2.Dto.NotSendedReports
{
    public class NotSentReportDto
    {
        public int FirmId { get; set; }

        public int DocumentId { get; set; }

        public int DocumentVersionId { get; set; }

        public ReportType ReportType { get; set; }

        public ReportPeriodType PeriodType { get; set; }

        public int PeriodNumber { get; set; }

        public int Year { get; set; }
    }
}
