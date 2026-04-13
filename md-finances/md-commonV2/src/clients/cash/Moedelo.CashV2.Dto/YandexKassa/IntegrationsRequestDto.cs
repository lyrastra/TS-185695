using System;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.CashV2.Dto.YandexKassa
{
    /// <summary>
    /// Запрос на получение выписки.
    /// </summary>
    public class IntegrationsRequestDto
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IntegrationPartners IntegrationPartner { get; set; }

        public int FirmId { get; set; }
    }
}