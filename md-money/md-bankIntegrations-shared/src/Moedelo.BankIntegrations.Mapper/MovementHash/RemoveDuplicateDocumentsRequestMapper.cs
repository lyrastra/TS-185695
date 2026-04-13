using Moedelo.BankIntegrations.ApiClient.Dto.MovementHash;
using Moedelo.BankIntegrations.Models.MovementHash;
using System.Linq;

namespace Moedelo.BankIntegrations.Mapper.MovementHash
{
    public static class RemoveDuplicateDocumentsRequestMapper
    {
        public static RemoveDuplicateDocumentsRequest Map(this RemoveDuplicateDocumentsRequestDto dto) 
        {
            return new RemoveDuplicateDocumentsRequest()
            {
                Content1C = dto.Content1C,
                FirmId = dto.FirmId,
                MovementHashes = dto.MovementHashes.Select(x => x.Map()).ToList(),
                PartnerId = dto.PartnerId
            };
        }
    }
}
