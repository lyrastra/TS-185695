using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform.IqDialer
{
    public class IqDialerCreateCommand
    {
        public string Phone { get; set; }
        public string Disposition { get; set; }
        public int? FirmId { get; set; }
        public int Attempts { get; set; }
    }
}