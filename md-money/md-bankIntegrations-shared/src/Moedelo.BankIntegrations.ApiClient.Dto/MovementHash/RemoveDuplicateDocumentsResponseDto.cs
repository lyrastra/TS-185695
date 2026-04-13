using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.MovementHash
{
    public class RemoveDuplicateDocumentsResponseDto
    {
        public string Content1C { get; set; }
        public List<bool> RemovedDocuments { get; set; }
    }
}
