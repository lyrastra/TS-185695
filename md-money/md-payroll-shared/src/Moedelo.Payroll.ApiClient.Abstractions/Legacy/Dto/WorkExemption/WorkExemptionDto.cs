using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkExemption
{
    public class WorkExemptionDto
    {
        /// <summary>
        /// Начало освобождения от работы
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// Окончание освобождения от работы
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// Должность врача
        /// </summary>
        public string DoctorPosition { get; set; }

        /// <summary>
        /// ФИО врача
        /// </summary>
        public string DoctorFullName { get; set; }

        /// <summary>
        /// Идентификационные номер врача
        /// </summary>
        public string DoctorIdentityNumber { get; set; }

        /// <summary>
        /// ФИО председателя врачебной комиссии
        /// </summary>
        public string ChairmanFullName { get; set; }

        /// <summary>
        /// Идентификационный номер председателя врачебной комиссии
        /// </summary>
        public string ChairmanIdentityNumber { get; set; }
    }
}