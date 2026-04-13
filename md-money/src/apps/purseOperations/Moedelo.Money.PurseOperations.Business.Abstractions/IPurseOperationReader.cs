using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PurseOperations.Business.Abstractions.Models;

namespace Moedelo.Money.PurseOperations.Business.Abstractions
{
    public interface IPurseOperationReader
    {
        Task<OperationType> GetOperationTypeAsync(long documentBaseId);

        Task<PurseOperationResponse> GetByBaseIdAsync(long documentBaseId, OperationType operationType);

        Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}