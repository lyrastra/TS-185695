using System;

namespace Moedelo.HistoricalLogsV2.Dto
{
    public class TelemetryEventDto
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string EventName { get; set; }
    }
}