using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.CommonV2.EventBus.Erpt
{
    public class OnOutsourceSignatureReadyEvent
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        private OnOutsourceSignatureReadyEvent() { }

        public OnOutsourceSignatureReadyEvent(int firmId, int userId)
        {
            FirmId = firmId;
            UserId = userId;
        }
    }
}
