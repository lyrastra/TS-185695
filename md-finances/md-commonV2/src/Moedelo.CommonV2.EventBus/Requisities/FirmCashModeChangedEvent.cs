namespace Moedelo.CommonV2.EventBus.Requisities;

public sealed class FirmCashModeChangedEvent
{
    public int FirmId { get; set; }
    public bool IsManualCashMode { get; set; }
}
