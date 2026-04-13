using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.CommonV2.EventBus.Contragents
{
    public class KontragentsDeletedEvent
    {
        public int UserId { get; set; }
        public int FirmId { get; set; }
        public List<int> KontragentsIds { get; set; }
    }
}
