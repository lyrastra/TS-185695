namespace Moedelo.CommonV2.EventBus.Crm
{
    public class UpdateOutsourcingClientDataCommand
    {
        public int FirmId { get; set; }
        public int OutsourceClientId { get; set; }
        public string BusinessAssistantLogin { get; set; }
        public string AccountantLogin { get; set; }
        public string PayrollAccountantLogin { get; set; }
        public string DocsSpecialistLogin { get; set; }
        public string AssistantLogin { get; set; }
        public string StaffSpecialistLogin { get; set; }
        public string TeamLeaderLogin { get; set; }
        public bool IsLost { get; set; }
    }
}
