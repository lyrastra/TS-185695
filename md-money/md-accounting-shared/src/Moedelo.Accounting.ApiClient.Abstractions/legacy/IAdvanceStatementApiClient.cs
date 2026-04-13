using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface IAdvanceStatementApiClient
    {
        Task<AdvanceStatementDto> GetByBaseIdAsync(FirmId firmId, UserId userId, long documentBaseId);

        Task<AdvanceStatementDto[]> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds);
    }
}