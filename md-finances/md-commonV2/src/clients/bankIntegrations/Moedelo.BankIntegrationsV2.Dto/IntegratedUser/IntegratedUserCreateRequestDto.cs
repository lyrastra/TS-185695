using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedUser
{
    /// <summary>
    /// Запрос на создание записи об интеграции пользователя с внешней системой 
    /// </summary>
    public class IntegratedUserCreateRequestDto
    {
        /// <summary> Идентификатор фирмы </summary>
        public int FirmId { get; set; }
        /// <summary> Флаг, включена ли интеграция с сервисом </summary>
        public bool IsActive { get; set; }
        /// <summary> С кем интеграция </summary>
        public IntegrationPartners IntegrationPartner { get; set; }
        /// <summary> Дополнительные данные по интеграции (в формате JSON) </summary>
        public string IntegrationData { get; set; }
        /// <summary> Идентификатор пользователя во внешней системе </summary>
        public string ExternalClientId { get; set; }
    }
}
