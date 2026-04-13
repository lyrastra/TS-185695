using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.BankOperation
{
    public class IntegrationIdentityDto
    {
        public int FirmId { get; set; }

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