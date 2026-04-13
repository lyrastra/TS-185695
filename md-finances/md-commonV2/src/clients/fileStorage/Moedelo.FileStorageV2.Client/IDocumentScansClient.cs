using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.FileStorageV2.Client
{
    public interface IDocumentScansClient : IDI
    {
        Task DeleteDocumentScansAsync(int firmId, int userId, long baseId, bool ignoreError);

        Task DeleteDocumentsScansAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, bool ignoreError);

        Task<List<string>> GetDocumentScansAsync(int firmId, int userId, long baseId);

        Task UpdateJustCreatedDocumentScansPathsAsync(int firmId, int userId, long temporaryBaseId, long baseId);
    }
}