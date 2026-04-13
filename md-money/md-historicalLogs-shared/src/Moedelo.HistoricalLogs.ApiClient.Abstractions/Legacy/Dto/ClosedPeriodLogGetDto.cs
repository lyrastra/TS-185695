namespace Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy.Dto
{
    public class ClosedPeriodLogGetDto
    {

        public int FirmId { get; set; }

        public int Limit { get; set; } = 20;

        public int Offset { get; set; } = 0;

    }
}
