using System;
using System.Collections.Generic;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class PartnerIntegrationRequestDailyWithoutRepeatClaimDto
    {
        public string DateOfCallAfter { get; set; }
        public IReadOnlyCollection<IntegrationCallType> Types { get; set; }
    }
}
