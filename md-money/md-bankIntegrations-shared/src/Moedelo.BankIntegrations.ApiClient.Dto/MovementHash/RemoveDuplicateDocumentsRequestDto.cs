using Moedelo.BankIntegrations.ApiClient.Dto.MovementHashService;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.MovementHash
{
    public class RemoveDuplicateDocumentsRequestDto
    {
        public List<MovementHashDto> MovementHashes { get; set; }
        public string Content1C { get; set; }
        public int FirmId { get; set; }
        public int PartnerId { get; set; }
    }
}
