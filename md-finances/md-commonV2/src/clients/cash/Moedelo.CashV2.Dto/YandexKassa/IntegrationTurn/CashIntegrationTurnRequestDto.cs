using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.CashV2.Dto.YandexKassa.IntegrationTurn
{
    public class CashIntegrationTurnRequestDto
    {
        /// <summary> Партнёр </summary>
        public IntegrationPartners IntegrationPartner { get; set; }

        /// <summary> Статус интеграции </summary>
        public bool IsOn { get; set; }

        /// <summary> Идентификатор пользователя во внешней системе </summary>
        public string ExternalClientId { get; set; }
    }
}
