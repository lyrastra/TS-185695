using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Registry.Domain.Models.OperationTypeSumByPeriod;

public class OperationTypeSumByPeriodRequest
{
    /// <summary>
    /// Типы операций
    /// </summary>
    public OperationType[] OperationTypes { get; set; }

    /// <summary>
    /// Фильтр по дате операции: с указанной даты (включительно)
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Фильтр по дате операции: до указанной даты (включительно)
    /// </summary>
    public DateTime EndDate { get; set; }
}