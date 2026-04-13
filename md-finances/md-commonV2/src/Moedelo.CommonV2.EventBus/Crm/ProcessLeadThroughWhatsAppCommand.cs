namespace Moedelo.CommonV2.EventBus.Crm
{
    public class ProcessLeadThroughWhatsAppCommand
    { 
        public int FirmId { get; set; }
        public int? RegistrationHistoryId { get; set; }
        public string FunnelCode { get; set; }
    }
}
