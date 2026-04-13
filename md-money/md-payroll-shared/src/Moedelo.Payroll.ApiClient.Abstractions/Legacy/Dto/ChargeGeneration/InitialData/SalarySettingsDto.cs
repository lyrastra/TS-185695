using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalarySettings;
using Moedelo.Payroll.Enums.SalarySettings;
using Moedelo.Payroll.Shared.Enums.SalarySettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class SalarySettingsDto
{
    /// <summary>
    /// День выплаты зарплаты
    /// </summary>
    public int DaySalaryPayment { get; set; }

    /// <summary>
    /// день выплаты аванса
    /// </summary>
    public int DayAdvancePayment { get; set; }

    /// <summary>
    /// Период выплаты зарплаты: в расчетном месяце или следующим
    /// </summary>
    public SalaryPaymentPeriod SalaryPaymentPeriod { get; set; }

    /// <summary>
    /// Тип расчета аванса
    /// </summary>
    public AdvanceCalculationType AdvanceCalculationType { get; set; }

    /// <summary>
    /// С данной даты участвовать в пилотном проекте
    /// </summary>
    public DateTime? PilotProjectStartDate { get; set; }
}