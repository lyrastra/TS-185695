using System;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.Mobile
{
    public class DataForIntegrationRequestDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int FirmId { get; set; }

        public string Inn { get; set; }

        public string SettlementNumber { get; set; }

        public string Bik { get; set; }

        public IntegrationPartners IntegrationPartner { get; set; }

        public bool IsManual { get; set; }
    }
}