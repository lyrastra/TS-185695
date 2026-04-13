using System;
using Moedelo.Requisites.Enums.NdsRatePeriods;

namespace Moedelo.Requisites.Kafka.Abstractions.NdsRatePeriods.Events;

/// <summary>
/// Настройка (для УСН): Применяемая ставка НДС
/// </summary>
public class NdsRatePeriodEventData
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
    /// Тип ставки
    /// </summary>
    public NdsRateType Rate { get; set; }
}