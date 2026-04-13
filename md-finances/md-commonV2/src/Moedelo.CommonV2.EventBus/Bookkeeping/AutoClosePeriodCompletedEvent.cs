using System;

namespace Moedelo.CommonV2.EventBus.Bookkeeping;

/// <summary>
/// Автоматическое закрытие месяца (> 3 месяцев, по одному за раз) завершено 
/// </summary>
public class AutoClosePeriodCompletedEvent
{
    public int FirmId { get; set; }

    public int UserId { get; set; }

    public DateTime ClosedDate { get; set; }
}