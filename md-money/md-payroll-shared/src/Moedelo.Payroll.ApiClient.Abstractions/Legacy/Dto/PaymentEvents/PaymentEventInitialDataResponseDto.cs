using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;

public class PaymentEventInitialDataResponseDto
{
    /// <summary>
    /// Данные зарплатного проекта
    /// </summary>
    public PaymentEventSalaryProjectDto SalaryProject { get; set; }

    /// <summary>
    /// Данные настроек зарплаты
    /// </summary>
    public PaymentEventSalarySettingsDto SalarySettings { get; set; }

    /// <summary>
    /// Данные сотрудников
    /// </summary>
    public IReadOnlyList<PaymentEventWorkerModelDto> Workers { get; set; }

    /// <summary>
    /// Начисления
    /// </summary>
    public IReadOnlyList<PaymentEventWorkerChargeDto> Charges { get; set; }

    /// <summary>
    /// Удержания
    /// </summary>
    public IReadOnlyList<PaymentEventDeductionDto> Deductions { get; set; }
}