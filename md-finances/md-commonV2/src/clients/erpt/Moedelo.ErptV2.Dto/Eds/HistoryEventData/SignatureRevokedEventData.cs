using System;
using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.Eds.HistoryEventData
{
    public class SignatureRevokedEventData
    {
        public DateTime Date { get; set; }
        public string SerialNumber { get; set; }
        public string Thumbprint { get; set; }
        public IReadOnlyList<string> PacketGuids { get; set; }
    }
}
