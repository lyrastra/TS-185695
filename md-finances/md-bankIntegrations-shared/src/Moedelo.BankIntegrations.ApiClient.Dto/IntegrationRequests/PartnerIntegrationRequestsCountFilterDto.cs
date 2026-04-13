using System;
using System.Collections.Generic;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class PartnerIntegrationRequestsCountFilterDto
    {
        public IReadOnlyCollection<IntegrationPartners> Partners { get; set; }
        public IReadOnlyCollection<RequestStatus> Statuses { get; set; }
        public string MinDateOfCall { get; set; }
        public string MaxDateOfCall { get; set; }
        public bool? IsManual { get; set; }
        public int? MinCount { get; set; }
        public IReadOnlyCollection<IntegrationCallType> Types { get; set; }
        public string ExcludedEndDate { get; set; }
    }
}
