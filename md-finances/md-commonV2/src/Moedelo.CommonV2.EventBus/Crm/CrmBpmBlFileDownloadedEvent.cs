namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmBpmBlFileDownloadedEvent : CrmBpmBlFileDownloadRequestEvent
    {
        public int? FileId { get; set; }
    }
}