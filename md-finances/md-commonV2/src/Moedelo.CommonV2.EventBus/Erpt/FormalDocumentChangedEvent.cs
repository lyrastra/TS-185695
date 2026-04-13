using Moedelo.Common.Enums.Enums.Reports;

namespace Moedelo.CommonV2.EventBus.Erpt
{
    public class FormalDocumentChangedEvent
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public int DocumentId { get; set; }

        public int DocumentVersionId { get; set; }
        
        public ReportStatus? ReportStatus { get; set; }
    }
}
