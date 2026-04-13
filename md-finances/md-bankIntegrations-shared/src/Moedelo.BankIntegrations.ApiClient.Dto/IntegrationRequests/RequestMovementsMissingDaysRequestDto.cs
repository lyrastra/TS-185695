using System;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class RequestMovementsMissingDaysRequestDto
    {
        public IntegrationPartners Partner { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
