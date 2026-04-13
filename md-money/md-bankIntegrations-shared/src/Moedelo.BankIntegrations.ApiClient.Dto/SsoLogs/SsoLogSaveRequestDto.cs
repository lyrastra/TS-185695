using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SsoLogs
{
    public class SsoLogSaveRequestDto
    {
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

        /// <summary>
        /// Лог запроса
        /// </summary>
        public byte[] LogContent { get; set; }
    }
}
