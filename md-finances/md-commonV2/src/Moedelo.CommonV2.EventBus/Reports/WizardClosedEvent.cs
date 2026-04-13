using Moedelo.Common.Enums.Enums.Wizard;

namespace Moedelo.CommonV2.EventBus.Reports
{
    public class WizardClosedEvent
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public long WizardId { get; set; }

        public WizardType WizardType { get; set; }
    }
}