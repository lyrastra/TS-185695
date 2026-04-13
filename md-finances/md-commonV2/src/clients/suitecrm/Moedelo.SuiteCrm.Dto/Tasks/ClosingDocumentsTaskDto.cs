namespace Moedelo.SuiteCrm.Dto.Tasks
{
    public class ClosingDocumentsTaskDto
    {
        /// <summary>
        /// ИД фирмы.
        /// </summary>
        public int FirmId { get; set; }
        
        /// <summary>
        /// Емейл сотрудника, на которого будет назначена задача.
        /// </summary>
        public string AssignedUserEmail { get; set; }

        /// <summary>
        /// Тема задачи.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Описание задачи.
        /// </summary>
        public string Description { get; set; }
    }
}