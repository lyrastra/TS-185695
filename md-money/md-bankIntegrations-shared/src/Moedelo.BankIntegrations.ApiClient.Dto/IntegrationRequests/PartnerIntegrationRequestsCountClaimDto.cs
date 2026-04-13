using System;
using System.Collections.Generic;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class PartnerIntegrationRequestsCountClaimDto
    {
        /// <summary>
        /// только для этой фирмы
        /// </summary>
        public int? FirmId { get; set; }
        public IReadOnlyCollection<IntegrationPartners> Partners { get; set; }
        public IReadOnlyCollection<RequestStatus> Statuses { get; set; }
        public IReadOnlyCollection<IntegrationCallType> Types { get; set; }
        public string DateOfCallAfter { get; set; }
        public string DateOfCallBefore { get; set; }
        public bool? IsManual { get; set; }
    }
}
