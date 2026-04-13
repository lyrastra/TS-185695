namespace Moedelo.CommonV2.EventBus.Erpt
{
    public class NeformalDocumentsLoadAutomaticallyEvent
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }
        
        public int NeformalDocumentId { get; set; }
    }
}