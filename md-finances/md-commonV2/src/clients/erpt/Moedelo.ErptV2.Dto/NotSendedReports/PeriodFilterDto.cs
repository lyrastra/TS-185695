namespace Moedelo.ErptV2.Dto.NotSendedReports
{
    public class PeriodFilterDto
    {
        public int Year { get; set; }

        public ReportPeriodType? PeriodType { get; set; }

        public int PeriodNumber { get; set; }
    }
}
