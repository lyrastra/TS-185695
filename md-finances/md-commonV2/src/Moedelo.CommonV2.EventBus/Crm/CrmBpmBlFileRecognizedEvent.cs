namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmBpmBlFileRecognizedEvent : CrmBpmBlFileRecognizeRequestEvent
    {
        public bool Result { get; set; }
    }
}