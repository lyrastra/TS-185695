using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using System;

namespace Moedelo.BankIntegrationsV2.Dto.SsoLogs
{
    public class SsoLogRequestDto
    {
        /// <summary>
        /// Идентификатор лога запроса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата запроса
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Статус запроса, 0 - успешно
        /// </summary>
        public int RequestStatus { get; set; }

        /// <summary>
        /// Банк- партнер
        /// </summary>
        public IntegrationPartners IntegrationPartner { get; set; }

        /// <summary>
        /// Идентификатор клиента, пока используем ИНН
        /// </summary>
        public string ClientId { get; set; }
    }
}
