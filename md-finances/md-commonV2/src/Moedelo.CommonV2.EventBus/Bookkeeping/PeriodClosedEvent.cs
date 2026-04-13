using Moedelo.Common.Enums.Enums.ClosingPeriod;

namespace Moedelo.CommonV2.EventBus.Bookkeeping;

/// <summary>
/// Закрытие периода
/// </summary>
public class PeriodClosedEvent
{
    public int FirmId { get; set; }

    public int UserId { get; set; }

    /// <summary>
    /// Каким способом закрыт период
    /// </summary>
    public CloseType CloseType { get; set; }
}