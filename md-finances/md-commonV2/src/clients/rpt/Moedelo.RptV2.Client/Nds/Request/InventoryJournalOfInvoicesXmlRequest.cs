using System.Collections.Generic;

namespace Moedelo.RptV2.Client.Nds.Request
{
    public class InventoryJournalOfInvoicesXmlRequest
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public IEnumerable<string> JournalNames { get; set; }
    }
}
