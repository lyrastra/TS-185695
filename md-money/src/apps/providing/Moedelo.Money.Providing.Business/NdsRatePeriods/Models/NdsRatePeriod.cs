using System;
using Moedelo.Requisites.Enums.NdsRatePeriods;

namespace Moedelo.Money.Providing.Business.NdsRatePeriods.Models;

/// <summary>
/// Настройка в УП для УСН: применяемый НДС (период действия)
/// </summary>
public class NdsRatePeriod
{
    public NdsRateType Rate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}