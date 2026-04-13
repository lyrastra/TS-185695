using System;

namespace Moedelo.OfficeV2.Dto.Aggregator
{
    public class GenProcCheckInfoDto
    {
        /// <summary>
        /// Название организации
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// ОГРН организации
        /// </summary>
        public string Ogrn { get; set; }

        /// <summary>
        /// ИНН организации
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// Место нахождения объекта
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Проверяющий орган
        /// </summary>
        public string InspectionBody { get; set; }

        /// <summary>
        /// Начало проверки
        /// </summary>
        public DateTime InspectionStartDate { get; set; }

        /// <summary>
        /// Продолжительность проверки
        /// </summary>
        public string InspectionDuration { get; set; }

        /// <summary>
        /// Цель проверки
        /// </summary>
        public string InspectionReason { get; set; }

        /// <summary>
        /// Плановая/неплановая проверки
        /// </summary>
        public bool IsPlanned { get; set; }
    }
}