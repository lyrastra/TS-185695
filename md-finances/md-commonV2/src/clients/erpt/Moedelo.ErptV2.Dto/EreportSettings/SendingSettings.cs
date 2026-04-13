using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.EreportSettings
{
    public class SendingSettings
    {
        public SendingStatus Status { get; set; } 
        public ConfirmationMethod ConfirmationMethod { get; set; }
        public List<SendingDirection> Directions { get; set; }
        public DocumentTransportProvider SendProvider { get; set; }
        public bool IsQrCodeActivated { get; set; }

        public SendingSettings()
        {
            Directions = new List<SendingDirection>();
        }
    }
}
