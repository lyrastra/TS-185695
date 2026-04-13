namespace Moedelo.CommonV2.EventBus.Erpt
{
    public class NeformalDocumentsLoadManualyEvent
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }
        
        public int NeformalDocumentId { get; set; }
    }
}