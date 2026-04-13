namespace Moedelo.CommonV2.EventBus.Bookkeeping;

/// <summary>
/// Открытие периода
/// </summary>
public class PeriodOpenedEvent
{
    public int FirmId { get; set; }

    public int UserId { get; set; }
}