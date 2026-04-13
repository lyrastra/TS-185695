using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation
{
    public class BackOfficeRequestInfoDto
    {
        /// <summary> Индентификатор запроса в БД </summary>
        public int Id { get; set; }

        /// <summary> Статус запроса </summary>
        public int RequestStatus { get; set; }

        /// <summary> Какому банку отправили запрос </summary>
        public IntegrationPartners IntegrationPartner { get; set; }

        /// <summary> Дата начала периода </summary>
        public string StartDate { get; set; }

        /// <summary> Дата окончания периода </summary>
        public string EndDate { get; set; }

        /// <summary> Расчетный счет </summary>
        public string SettlementNumber { get; set; }

        /// <summary> Запрос выполнен вручную </summary>
        public bool IsManual { get; set; }

        /// <summary> Если банк не смог обработать запрос, пишем сюда вернувшеюся ошибку, если смог- выписку в формате Альфа-Банка </summary>
        public string Error { get; set; }

        /// <summary> Фактическая дата запроса </summary>
        public string DateOfCall { get; set; }

        /// <summary> Xml запроса </summary>
        public string RequestXml { get; set; }

        /// <summary> XML ответа </summary>
        public string ResponseXml { get; set; }
    }
}