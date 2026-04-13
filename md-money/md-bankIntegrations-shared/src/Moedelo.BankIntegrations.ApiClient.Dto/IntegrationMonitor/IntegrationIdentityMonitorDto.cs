using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class IntegrationIdentityMonitorDto
    {
        public string ExternalRequestId { get; set; }
        public int FirmId { get; set; }
        public string Login { get; set; }
        public string Kpp { get; set; }
        public string Inn { get; set; }
        public string SettlementNumber { get; set; }
        public string Bik { get; set; }
        public int IntegrationPartner { get; set; }
        /// <summary> Признак Учётки </summary>
        public bool IsAccounting { get; set; }
        /// <summary> Дополнительные параметры </summary>
        public Dictionary<string, object> SpecificParameters { get; set; } = new Dictionary<string, object>();
    }
}
