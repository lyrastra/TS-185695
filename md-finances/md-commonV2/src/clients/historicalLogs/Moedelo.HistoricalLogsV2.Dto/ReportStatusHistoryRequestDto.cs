using System;
using Moedelo.Common.Enums.Enums.Reports;

namespace Moedelo.HistoricalLogsV2.Dto
{
    public class ReportStatusHistoryRequestDto
    {
        public int FirmId { get; set; }
        public int CalendarId { get; set; }
        public DateTime ChangeDate { get; set; }
        public OwnershipForm OwnershipForm { get; set; }
        public string OutsourcingGroup { get; set; }
        public TaxationSystem TaxationSystem { get; set; }
        public ServiceType ServiceType { get; set; }
        public int TariffId { get; set; }
        public int LegalUserId { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public string RejectReason { get; set; }
        public bool IsManualReport { get; set; }
    }
}