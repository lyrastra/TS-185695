using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Pledges
{
    /// <summary>
    /// Информация о залогодержателях 
    /// </summary>
    public class PledgerPledgeeResponseDto
    {
        /// <summary>
        /// Форма участника
        /// </summary>
        public PledgeeParticipantFormEnum? ParticipantForm { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        public string Ogrn { get; set; }

        /// <summary>
        /// Наименование или ФИО
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения (только для физических лиц)
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Субъект РФ (только для физических лиц
        /// </summary>
        public string RfSubject { get; set; }

        /// <summary>
        /// Паспорт (только для физических лиц)
        /// </summary>
        public string Passport { get; set; }
    }
}
