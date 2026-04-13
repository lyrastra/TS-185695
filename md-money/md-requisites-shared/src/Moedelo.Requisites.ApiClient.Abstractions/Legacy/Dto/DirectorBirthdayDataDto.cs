
using System;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    /// <summary>
    /// Общие данные для получения дней рождений руководителей фирм
    /// </summary>
    public class DirectorBirthdayDataDto
    {
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Наименование фирмы
        /// </summary>
        public string FirmName { get; set; }

        /// <summary>
        /// Признак ООО
        /// </summary>
        public bool IsOoo { get; set; }

        /// <summary>
        /// Фамилия (для ИП)
        /// </summary>
        public string IpSurname { get; set; }

        /// <summary>
        /// Имя (для ИП)
        /// </summary>
        public string IpName { get; set; }

        /// <summary>
        /// Отчество (для ИП)
        /// </summary>
        public string IpPatronymic { get; set; }

        /// <summary>
        /// Дата рождения (для ИП)
        /// </summary>
        public DateTime? IpDateOfBirth { get; set; }

        /// <summary>
        /// Номер дня рождения(для ИП)
        /// </summary>
        public int? IpBirthDay { get; set; }

        /// <summary>
        /// Номер месяца рождения(для ИП)
        /// </summary>
        public int? IpBirthMonth { get; set; }

        /// <summary>
        /// Идентификатор работника на должности Директор (для ООО)
        /// </summary>
        public int? DirectorId { get; set; }
    }
}
