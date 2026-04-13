using System.Collections.Generic;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.BankIntegrationsV2.Dto.Integrations
{
    public class IntegrationIdentityDto
    {
        public int FirmId { get; set; }
        
        public int UserId { get; set; }
        
        public string Inn { get; set; }

        public string SettlementNumber { get; set; }

        public string Bik { get; set; }

        public IntegrationPartners IntegrationPartner { get; set; }

        /// <summary> Признак Учётки </summary>
        public bool IsAccounting { get; set; }

        /// <summary> Дополнительные параметры </summary>
        public Dictionary<string, object> SpecificParameters { get; set; } = new Dictionary<string, object>();
    }
}
