namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings
{
    public enum VacationType
    {
        /// <summary>
        /// Основной
        /// </summary>
        Main = 0,

        /// <summary>
        /// Дополнительный 
        /// </summary>
        Additional = 1,

        /// <summary>
        /// Основной+дополнительный
        /// </summary>
        MainWithAdditional = 2,

        /// <summary>
        /// Без сохранения ЗП
        /// </summary>
        WithoutSalary = 3,
        
        /// <summary>
        /// Учебный
        /// </summary>
        Study = 4
    }
}