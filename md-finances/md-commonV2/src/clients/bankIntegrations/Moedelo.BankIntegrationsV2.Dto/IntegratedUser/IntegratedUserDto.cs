using System;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedUser
{
    /// <summary> Общий класс для сущности, обозначающей интеграцию пользователя с внешней системой </summary>
    public class IntegratedUserDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Id фирмы в "Моём деле" </summary>
        public int FirmId { get; set; }

        /// <summary> Логин пользователя в "Моём деле" </summary>
        public string Login { get; set; }

        /// <summary> ИНН </summary>
        public string Inn { get; set; }

        /// <summary> КПП </summary>
        public string Kpp { get; set; }

        /// <summary> Флаг, включена ли интеграция с сервисом </summary>
        public bool IsActive { get; set; }

        /// <summary> С кем интеграция </summary>
        public IntegrationPartners IntegrationPartner { get; set; }

        /// <summary> Json данные </summary>
        public string IntegrationData { get; set; }

        /// <summary> Идентификатор пользователя во внешней системе </summary>
        public string ExternalClientId { get; set; }

        /// <summary> Зарезервированное время (пишем сюда время включения интеграции) </summary>
        public DateTime CreateDate { get; set; }
    }
}