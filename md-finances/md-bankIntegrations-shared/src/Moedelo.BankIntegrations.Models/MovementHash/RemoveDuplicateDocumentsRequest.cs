using System.Collections.Generic;

namespace Moedelo.BankIntegrations.Models.MovementHash
{
    public class RemoveDuplicateDocumentsRequest
    {
        public List<MovementHash> MovementHashes { get; set; }
        public string Content1C { get; set; }
        public int FirmId { get; set; }
        public int PartnerId { get; set; }
    }
}
