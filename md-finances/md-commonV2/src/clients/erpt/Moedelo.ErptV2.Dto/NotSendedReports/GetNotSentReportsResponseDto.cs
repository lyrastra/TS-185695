namespace Moedelo.ErptV2.Dto.NotSendedReports
{
    public class GetNotSentReportsResponseDto
    {
        public NotSentReportDto[] Reports { get; set; }

        public int TotalCount { get; set; }
    }
}
