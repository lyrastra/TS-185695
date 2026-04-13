using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation
{
    public class IntegrationRequestQueueStatusDto
    {
        /// <summary> Идентификатор партнёра </summary>
        public virtual IntegrationPartners IntegratedPartner { get; set; }

        /// <summary> Есть ли необработанные запросы </summary>
        public bool HasUnprocessedRequests { get; set; }
    }
}