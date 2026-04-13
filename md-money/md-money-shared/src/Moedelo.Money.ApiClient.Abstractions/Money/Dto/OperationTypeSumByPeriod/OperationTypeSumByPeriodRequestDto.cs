using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto.OperationTypeSumByPeriod;

public class OperationTypeSumByPeriodRequestDto
{
    /// <summary>
    /// Фильтр по типам операций
    /// </summary>
    public OperationType[] OperationTypes { get; set; } // на начальном этапе нужны все операции кроме 2х (139, 140)

    /// <summary>
    /// Фильтр-исключение по типам операций
    /// </summary>
    public OperationType[] ExcludedOperationTypes { get; set; } // на начальном этапе нужны все операции кроме 2х (139, 140)

    /// <summary>
    /// Фильтр по дате: с указанной даты (включительно)
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Фильтр по дате: до указанной даты (включительно)
    /// </summary>
    public DateTime EndDate { get; set; }
}
