namespace Moedelo.CommonV2.EventBus.Account;

public class AccountFirmIsDeletedChangedEvent
{
    public int FirmId { get; set; }
    public bool IsDeleted { get; set; }
}
