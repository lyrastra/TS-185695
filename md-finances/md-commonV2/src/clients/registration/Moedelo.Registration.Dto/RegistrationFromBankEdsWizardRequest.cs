using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.Billing;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.Registration.Dto
{
    public class RegistrationFromBankEdsWizardRequest
    {
        public IntegrationPartners Partner { get; set; }

        public Tariff Tariff { get; set; }
        public IpEmployeeTypes IpEmployeeType { get; set; }

        public string Login { get; set; }
        public string Phone { get; set; }
        public string Inn { get; set; }

        public bool IsUsn { get; set; }
        public double UsnSize { get; set; }
        public UsnTypes UsnType { get; set; }

        public bool IsOnlyIpUsn6 { get; set; }
        public bool IsOsno { get; set; }
        public bool IsEnvd { get; set; }
        public bool IsPatent { get; set; }

        public string UtmSource { get; set; }
        public string UtmSourceExtension { get; set; }
    }
}
