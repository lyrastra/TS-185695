using System;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedFile
{
    public class IntegratedFileGeneralInfoDto
    {
        public int Id { get; set; }

        public DateTime FileDate { get; set; }

        public IntegrationPartners IntegrationPartner { get; set; }

        public bool IsEmpty { get; set; }
    }
}