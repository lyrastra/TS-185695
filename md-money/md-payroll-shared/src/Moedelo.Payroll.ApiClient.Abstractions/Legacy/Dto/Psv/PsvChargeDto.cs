namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Psv
{
    public class PsvChargeDto
    {
        public int WorkerId { get; set; }

        public decimal ChargeSum { get; set; }

        /// <summary>
        /// Отклонение от штатного расписания по которому было сгенерировано начисление
        /// </summary>
        public long? SpecialScheduleId { get; set; }

        /// <summary>
        /// Настройка выплат, по которой было сгенерировано начисление
        /// </summary>
        public long? WorkerSalarySettingId { get; set; }
    }
}