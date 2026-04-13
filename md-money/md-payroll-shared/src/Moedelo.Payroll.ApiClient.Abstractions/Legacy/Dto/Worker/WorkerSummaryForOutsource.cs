namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    /// <summary>
    /// Статистика по сотрудникам для BPM
    /// </summary>
    public class WorkersSummaryForOutsourceDto
    {
        /// <summary>
        /// Кол-во штатных сотрудников 
        /// </summary>
        public int StaffCount { get; set; }
        
        /// <summary>
        /// Кол-во сотрудников на ГПД
        /// </summary>
        public int NotStaffCount { get; set; }
        
        /// <summary>
        /// Кол-во иностранных сотрудников
        /// </summary>
        public int ForeignerCount { get; set; }
    }
}