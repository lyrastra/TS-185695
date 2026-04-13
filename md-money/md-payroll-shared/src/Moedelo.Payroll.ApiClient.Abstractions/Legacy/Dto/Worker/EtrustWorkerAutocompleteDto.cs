using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class EtrustWorkerAutocompleteDto
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// СНИЛС
        /// </summary>
        public string Snils { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Иностранный сотрудник
        /// </summary>
        public bool IsForeigner { get; set; }

        /// <summary>
        /// Дата выдачи документа удостоверяющего личность
        /// </summary>
        public DateTime? IdDocDate { get; set; }

        /// <summary>
        /// Номер документа удостоверяющего личность
        /// </summary>
        public string IdDocNumber { get; set; }

        /// <summary>
        /// Код подразделения документа удостоверяющего личность
        /// </summary>
        public string IdDocDepartmentCode { get; set; }

        /// <summary>
        /// Поле "Кем выдан" документа удостоверяющего личность
        /// </summary>
        public string IdDocIssuer { get; set; }

        public string CitizenshipCode { get; internal set; }
    }
}
