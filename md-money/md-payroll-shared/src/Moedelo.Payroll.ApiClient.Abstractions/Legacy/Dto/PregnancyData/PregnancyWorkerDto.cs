using System;
using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PregnancyData
{
    public class PregnancyWorkerDto
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
        public List<PregnancyForProlongDto> PregnancyForProlong { get; set; } = new List<PregnancyForProlongDto>();

        public List<WorkerCalculationIllnessYearDto> Years { get; set; } = new List<WorkerCalculationIllnessYearDto>();
    }
}