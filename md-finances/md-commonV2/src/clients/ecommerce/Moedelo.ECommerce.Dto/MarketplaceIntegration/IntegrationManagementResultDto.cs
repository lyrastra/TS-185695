using System;

namespace Moedelo.ECommerce.Dto.MarketplaceIntegration
{
    public class IntegrationManagementResultDto
    {
        public bool Status { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime? CredentialsExpirationDate { get; set; }
    }
}