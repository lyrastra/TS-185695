using System;
using System.Collections.Generic;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Registry.Dto.OperationTypeSumByPeriod;

public class OperationTypeSumByPeriodRequestDto
{
    /// <summary>
    /// Фильтр по типам операций
    /// </summary>
    public IReadOnlyCollection<OperationType> OperationTypes { get; set; }

    /// <summary>
    /// Фильтр по дате: с указанной даты (включительно)
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Фильтр по дате: до указанной даты (включительно)
    /// </summary>
    public DateTime EndDate { get; set; }
}