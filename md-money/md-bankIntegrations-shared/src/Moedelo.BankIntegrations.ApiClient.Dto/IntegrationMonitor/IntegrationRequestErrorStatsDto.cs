using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequest
{
    public class IntegrationRequestErrorStatsDto
    {
        public IntegrationPartners IntegrationPartner;
        /// <summary> Статус запросов из dbo.IntegrationsRequests
        public byte RequestStatus { get; set; }
        /// <summary> кол-во запросов из dbo.IntegrationsRequests
        public int Qty { get; set; }
    }
}
