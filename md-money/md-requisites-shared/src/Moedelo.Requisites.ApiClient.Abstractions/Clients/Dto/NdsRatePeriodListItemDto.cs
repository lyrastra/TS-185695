using System;
using Moedelo.Requisites.Enums.NdsRatePeriods;

namespace Moedelo.Requisites.ApiClient.Abstractions.Clients.Dto;

/// <summary>
/// Настройка (для УСН): Применяемая ставка НДС
/// </summary>
public class NdsRatePeriodListItemDto
{
    public int Id { get; set; }
    /// <summary>
    /// Период действия: начало (включительно)
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Период действия: конец (включительно)
    /// </summary>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// Тип ставка
    /// </summary>
    public NdsRateType Rate { get; set; }
    /// <summary>
    /// Ставка находится в закрытом периоде
    /// </summary>
    public bool HasClosedPeriod { get; set; }
}
