namespace Moedelo.Payroll.Shared.Enums.SalarySettings;

public enum AdvanceCalculationType
{
    /// <summary>
    /// Аванс начисляется от оклада вне зависимости от реально отработанных дней сотрудником за месяц
    /// </summary>
    FromFixedSalary = 0,

    /// <summary>
    /// Аванс начисляется в зависимости от реально отработанных дней (часов)
    /// сотрудником за месяц
    /// </summary>
    DependFromWorkDays = 1,
}