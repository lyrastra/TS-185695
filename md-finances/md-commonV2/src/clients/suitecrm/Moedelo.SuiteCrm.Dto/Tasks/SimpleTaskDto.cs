using Moedelo.Common.Enums.Enums.Crm;

namespace Moedelo.SuiteCrm.Dto.Tasks
{
    public class SimpleTaskDto
    {
        public int FirmId { get; set; }

        public string Phone { get; set; }

        public string Subject { get; set; }

        public TaskPriorityDto Priority { get; set; }

        public CrmTaskType Type { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Признак: необходимо закрепить задачу за менеджером по обучению БИЗ
        /// </summary>
        public bool IsForBizEducationManagerTask { get; set; }

        /// <summary>
        /// Признак: необходимо закрепить задачу за ответственным по управленческому учёту
        /// </summary>
        public bool IsAssignTaskWithOwnerFromUU { get; set; }

        /// <summary>
        /// Признак: необходимо закрепить задачу за ответственным по отделу ИБ
        /// </summary>
        public bool IsAssignTaskWithOwnerFromIB { get; set; }
        
        /// <summary>
        /// Признак: необходимо закрепить задачу за ответственным по отделу продаж
        /// </summary>
        public bool IsAssignTaskWithSalesManager { get; set; }
    }
}