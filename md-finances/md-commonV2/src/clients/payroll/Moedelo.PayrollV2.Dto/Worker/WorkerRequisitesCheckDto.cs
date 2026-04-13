using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.PayrollV2.Dto.Worker
{
    public class WorkerRequisitesCheckDto
    {
        public int Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Fio { get; set; }
        
        /// <summary>
        /// Статус заполнения реквизитов сотрудника
        /// </summary>
        public WorkerRequisitesStatus RequisitesStatus { get; set; }
    }
}