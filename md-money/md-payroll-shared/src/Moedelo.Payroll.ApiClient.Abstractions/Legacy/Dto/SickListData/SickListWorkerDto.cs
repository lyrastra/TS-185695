using System;
using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListWorkerDto
    {
        /// <summary>
        /// Фамилия, имя, отчество сотрудника
        /// </summary>
        public string WorkerFio { get; set; }
        
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int WorkerId { get; set; }
        
        /// <summary>
        /// Дата начала работы
        /// </summary>
        public DateTime? DateOfStartWork { get; set; }

        /// <summary>
        /// СНИЛС
        /// </summary>
        public string Snils { get; set; }

        /// <summary>
        /// Сотрудник вне штата
        /// </summary>
        public bool IsNotStaff { get; set; }

        /// <summary>
        /// Список больничных для продолжения
        /// </summary>
        public List<SickListForProlongDto> SickListProlong { get; set; } = new List<SickListForProlongDto>();

        public List<WorkerCalculationIllnessYearDto> Years { get; set; } = new List<WorkerCalculationIllnessYearDto>();
    }
}