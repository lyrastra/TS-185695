using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    /// <summary>
    /// Привязка диалога (Request) к фирме (Firm)
    /// </summary>
    public class BindRequestToFirmEvent
    {
        public Guid RequestId { get; set; }
        public string Login { get; set; }
        public int? UserId { get; set; }
        public int? FirmId { get; set; }
    }
}
