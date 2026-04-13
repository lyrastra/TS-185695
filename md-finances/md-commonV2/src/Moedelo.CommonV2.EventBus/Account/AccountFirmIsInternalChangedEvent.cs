namespace Moedelo.CommonV2.EventBus.Account;

public class AccountFirmIsInternalChangedEvent
{
    public int FirmId { get; set; }
    public bool IsInternal { get; set; }
}