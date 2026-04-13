using System;

namespace Moedelo.Eds.Dto.EdsStatus
{
    public sealed class DelayedEdsRegistration
    {
        public int FirmId { get; set; }
        public DateTime EventDate { get; set; }
        public string FirmName { get; set; }
        public string Inn { get; set; }
        public string Login { get; set; }
        public string Comment { get; set; }
    }
}