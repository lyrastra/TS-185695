namespace Moedelo.SuiteCrm.Dto.Case
{
    public class CaseDto
    {
        /// <summary>
        /// ИД фирмы.
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Емейл сотрудника, на которого будет назначено обращение.
        /// </summary>
        public string AssignedUserEmail { get; set; }

        /// <summary>
        /// Тема обращения.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Описание обращения.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Приоритет. Часть полей указана в ExternalEnumCasePriority
        /// </summary>
        public string PriorityId { get; set; }

        /// <summary>
        /// Состояние обращения. Часть полей указана в ExternalEnumCaseState
        /// </summary>
        public string StateId { get; set; }

        /// <summary>
        /// Тип обращения. Часть полей указана в ExternalEnumCaseType
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// Статус обращения. Часть полей указана в ExternalEnumCaseStatus
        /// </summary>
        public string StatusId { get; set; }

        /// <summary>
        /// Категория обращения. Часть полей указана в ExternalEnumCaseCategory
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Подкатегория обращения. Часть полей указана в ExternalEnumCaseSubCategory
        /// </summary>
        public string SubCategoryId { get; set; }
    }
}
