namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings
{
    /// <summary>
    /// Типы отпуска
    /// </summary>
    public enum SpecialScheduleVacationCode
    {
        /// <summary> Ежегодный оплач.отпуск </summary>
        General = 0101,

        /// <summary> Отпуск без сохранения заработной платы </summary>
        WithoutSalary = 0103,

        /// <summary> учебный </summary>
        Study = 0105
    }
}