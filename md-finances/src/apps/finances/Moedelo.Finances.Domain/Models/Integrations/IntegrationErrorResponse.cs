using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Integration;
using IntegrationErrorType = Moedelo.BankIntegrations.Enums.IntegrationErrorType;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    public class IntegrationErrorResponse
    {
        public string Message { get; set; }

        public List<int> ErrorIds { get; set; }
        
        public IntegrationPartners IntegrationPartnerId { get; set; }

        public IntegrationErrorType ErrorType { get; set; }
        
        public IntegrationNotificationErrorType IntegrationNotificationErrorType { get; set; }
        
        public bool NeedLink { get; set; }
    }
}