using System;

namespace Moedelo.HistoricalLogsV2.Dto
{
    public class TelemetryLogEntryDto
    {
        public int EntryId { get; set; }
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public string EventName { get; set; }
        public string EventBody { get; set; }
        public DateTime CreateDate { get; set; }
        
    }
}