using System;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class SuiteCrmLeadLoadedEvent
    {
        public string Login { get; set; }

        public DateTime RegistrationInServiceDate { get; set; }

        public string Region { get; set; }

        public int FirmId { get; set; }
        public string FirmName { get; set; }

        public string TaxSystem { get; set; }

        public string AdditionalFio { get; set; }

        public string AdditionalPhone { get; set; }

        public string Ownership { get; set; }

        public int? MasterOfRegistrationStep { get; set; }
        
        public string UtmSource { get; set; }
        
        public string UtmExtension { get; set; }
        
        public string LeadGroup { get; set; }
    }
}