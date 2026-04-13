using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    /// <summary> Общий класс для сущности, обозначающей интеграцию пользователя с внешней системой </summary>
    public class IntegratedUserBaseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Id фирмы в "Моём деле" </summary>
        public int FirmId { get; set; }

        /// <summary> Флаг, включена ли интеграция с сервисом </summary>
        public bool IsActive { get; set; }

        /// <summary> С кем интеграция </summary>
        public int IntegrationPartner { get; set; }

        /// <summary> Зарезервированная строка </summary>
        public string IntegrationData { get; set; }

        /// <summary> Дата и время создания интеграции </summary>
        public DateTime CreateDate { get; set; }

        /// <summary> Внешний идентификатор </summary>
        public string ExternalClientId { get; set; }
        
        /// <summary> Информация по токенам </summary>
        public string TokenData { get; set; }
        
        /// <summary> Дата и время последнего включения интеграции </summary>
        public DateTime? EnabledDate { get; set; }
        
        /// <summary> Дата и время последнего выключения </summary>
        public DateTime? DisableDate { get; set; }
    }
}
