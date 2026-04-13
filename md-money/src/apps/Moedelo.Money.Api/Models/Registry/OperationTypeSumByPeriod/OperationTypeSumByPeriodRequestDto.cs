using System;
using System.Collections.Generic;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.Registry.OperationTypeSumByPeriod;

public class OperationTypeSumByPeriodRequestDto
{
    /// <summary>
    /// Фильтр по типам операций
    /// </summary>
    public IReadOnlyCollection<OperationType> OperationTypes { get; set; }

    /// <summary>
    /// Фильтр по дате: с указанной даты (включительно)
    /// </summary>
    [DateValue]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Фильтр по дате: до указанной даты (включительно)
    /// </summary>
    [DateValue]
    public DateTime EndDate { get; set; }
}