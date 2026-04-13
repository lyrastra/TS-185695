using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.Registry.OperationTypeSumByPeriod;

public class OperationTypeSumByPeriodRequest
{
    /// <summary>
    /// Фильтр по типам операций
    /// </summary>
    public OperationType[] OperationTypes { get; set; }

    /// <summary>
    /// Фильтр по дате: с указанной даты (включительно)
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Фильтр по дате: до указанной даты (включительно)
    /// </summary>
    public DateTime EndDate { get; set; }
}