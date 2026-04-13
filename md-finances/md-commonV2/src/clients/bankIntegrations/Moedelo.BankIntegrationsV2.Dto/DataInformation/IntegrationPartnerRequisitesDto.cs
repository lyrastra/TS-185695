using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation
{
    public class IntegrationPartnerRequisitesDto
    {
        public IntegrationPartners Id { get; set; }

        /// <summary> Имя партнера </summary>
        public string Name { get; set; }

        /// <summary> Текст ошибки в случае недоступности интеграции </summary>
        public string ErrorText { get; set; }

        /// <summary> Что вызывать для включения </summary>
        public string TurnOnText { get; set; }
    }
}
