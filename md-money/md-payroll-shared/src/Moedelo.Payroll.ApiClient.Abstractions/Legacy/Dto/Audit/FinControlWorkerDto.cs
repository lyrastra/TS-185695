namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Audit
{
    /// <summary>
    /// Модель сотрудника для интеграции с "Финансист" (md-ourPartners)
    /// </summary>
    public class FinControlWorkerDto
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// СНИЛС
        /// </summary>
        public string SocialInsuranceNumber { get; set; }

        /// <summary>
        /// Идентификатор cубконто
        /// </summary>
        public long SubcontoId { get; set; }
    }
}