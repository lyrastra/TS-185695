using System;

namespace Moedelo.CommonV2.EventBus.Account
{
    /// <summary>
    /// Изменилось закрепление фирмы за аккаунтом (стала главной или дочерней)
    /// </summary>
    public class FirmAccountChangedEvent
    {
        public int FirmId { get; set; }
        public string PreviousAccountName { get; set; }
        public string CurrentAccountName { get; set; }
        public DateTime Timestamp { get; set; }
        public int AuthorUserId { get; set; }
    }
}