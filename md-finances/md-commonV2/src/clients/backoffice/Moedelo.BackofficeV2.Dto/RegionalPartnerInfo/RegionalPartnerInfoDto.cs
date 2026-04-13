using System;
using Moedelo.Common.Enums.Enums.Partner;

namespace Moedelo.BackofficeV2.Dto.RegionalPartnerInfo
{
    public class RegionalPartnerInfoDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool TakesMoneyIntoHisOwnAccount { get; set; }
        public int UserId { get; set; }
        public RegionalPartnerType PartnerType { get; set; }

        /// <summary>
        /// Схема маршрутизации лидов
        /// </summary>
        public RegionalPartnerLeadRoutingType LeadRoutingType { get; set; }

        /// <summary>
        /// Ответственный менеджер
        /// </summary>
        public string LeadManagerId { get; set; }
        
        public string Phone { get; set; }
        
        public string Email { get; set; }

        public bool IsDeleted { get; set; }
        
        /// <summary> Идентификатор партнерской программы </summary>
        public int? PartnerProgramId { get; set; }

        /// <summary> Код партнерской программы </summary>
        public string PartnerProgramCode { get; set; }

        /// <summary> Доступность коммуникации (пуш) в мессенджеры </summary>
        public bool IsPushNotifyAllowed { get; set; }

        /// <summary> Дата заключения договора </summary>
        public DateTime? ContractSignDate { get; set; }
    }
}
