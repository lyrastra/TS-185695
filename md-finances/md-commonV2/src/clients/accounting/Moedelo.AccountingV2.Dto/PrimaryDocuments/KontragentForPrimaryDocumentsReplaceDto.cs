using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class KontragentForPrimaryDocumentsReplaceDto
    {
        public int NewKontragentId { get; set; }
        public string NewKontragentName { get; set; }
        public IReadOnlyCollection<int> OldKontragentIds { get; set; }
    }
}
