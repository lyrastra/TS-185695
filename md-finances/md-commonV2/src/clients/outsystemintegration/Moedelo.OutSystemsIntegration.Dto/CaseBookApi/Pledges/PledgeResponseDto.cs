using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Pledges
{
    /// <summary>
    /// Залог
    /// </summary>
    public class PledgeResponseDto
    {
        /// <summary>
        /// Тип залога
        /// </summary>
        public NoticeTypeEnum NoticeType { get; set; }
        
        /// <summary>
        /// Номер уведомления
        /// </summary>
        public string NoticeNumber { get; set; }

        /// <summary>
        /// Дата и время регистрации уведомления
        /// </summary>
        public DateTime? RegistrationDate { get; set; }

        /// <summary>
        /// Наименование договора о залоге
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// Дата договора о залоге
        /// </summary>
        public DateTime? ContractStartDate { get; set; }

        /// <summary>
        /// Номер договора о залоге
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// Срок исполнения обязательств
        /// </summary>
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// Информация о залогодателях 
        /// </summary>
        public PledgerPledgeeResponseDto[] Pledgers { get; set; }

        /// <summary>
        /// Информация о залогодержателях
        /// </summary>
        public PledgerPledgeeResponseDto[] Pledgees { get; set; }

        /// <summary>
        /// Информация о предметах залога
        /// </summary>
        public PledgeSubjectResponseDto[] Subjects { get; set; }
    }
}
