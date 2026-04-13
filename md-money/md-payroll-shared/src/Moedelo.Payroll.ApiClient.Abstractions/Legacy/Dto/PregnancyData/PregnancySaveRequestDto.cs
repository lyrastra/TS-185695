namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PregnancyData
{
    public class PregnancySaveRequestDto
    {
        /// <summary>
        /// Идентификатор продолжения декретного
        /// </summary>
        public long? ProlongSpecialScheduleId { get; set; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// Номер декретного
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// СНИЛС сотрудника
        /// </summary>
        public string Snils { get; set; }

        /// <summary>
        /// Данные листка нетрудоспособности
        /// </summary>
        public PregnancySaveDataDto PregnancyData { get; set; }

        /// <summary>
        /// Доход и расчёт декретного
        /// </summary>
        public PregnancyIncomeAndCalculationDto IncomeAndCalculation { get; set; }

        /// <summary>
        /// Версия
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Создано автоматически за пользователя
        /// </summary>
        public bool CreateOnBehalfOnUser { get; set; }
    }
}