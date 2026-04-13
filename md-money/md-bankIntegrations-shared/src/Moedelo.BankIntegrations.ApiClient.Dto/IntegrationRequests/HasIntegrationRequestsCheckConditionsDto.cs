using System;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class HasIntegrationRequestsCheckConditionsDto
    {
        public int? FirmId { get; set; }
        public string RequestId { get; set; } = null;
        public string SettlementNumber { get; set; } = null;
        public IntegrationPartners? Partner { get; set; }
        public RequestStatus[] Statuses { get; set; } = null;
        public RequestStatus[] NotInStatuses { get; set; } = null;
        public IntegrationCallType[] Types { get; set; } = null;
        public string MinDateOfCall { get; set; }
        public string MaxDateOfCall { get; set; }
        public bool? IsManual { get; set; }
    }
}
