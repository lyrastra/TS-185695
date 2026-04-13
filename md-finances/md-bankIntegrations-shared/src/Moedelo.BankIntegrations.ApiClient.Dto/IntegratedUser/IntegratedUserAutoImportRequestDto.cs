using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class IntegratedUserAutoImportRequestDto
    {
        public IntegrationPartners IntegratorId { get; set; } 
        public bool? IsActive { get; set; }
        public bool? IsAutoImport { get; set; }
    }
}
