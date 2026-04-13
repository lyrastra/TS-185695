namespace Moedelo.CommonV2.EventBus.Crm
{
    public class LeadLoadCommand
    {
        public int FirmId { get; set; }
        public bool ForceLoad { get; set; }
        public string NotifyEmailAddress { get; set; }
    }
}