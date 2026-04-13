namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListSaveRequestDto
    {
        /// <summary>
        /// Номер больничного
        /// </summary>
        public string SheetMainNumber { get; set; }

        /// <summary>
        /// Идентификатор продолжения больничного
        /// </summary>
        public long? ProlongSpecialScheduleId { get; set; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// СНИЛС
        /// </summary>
        public string Snils { get; set; }

        /// <summary>
        /// Данные листка нетрудоспособности
        /// </summary>
        public SickListSaveDataDto SickListData { get; set; }

        /// <summary>
        /// Доход и расчёт больничного
        /// </summary>
        public SickListIncomeAndCalculationDto IncomeAndCalculation { get; set; }

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