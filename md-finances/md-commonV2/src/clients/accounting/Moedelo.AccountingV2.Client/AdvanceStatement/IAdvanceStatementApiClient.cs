using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AdvanceStatement;
using Moedelo.AccountingV2.Dto.Api.ClientData;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccountingV2.Client.AdvanceStatement
{
    public interface IAdvanceStatementApiClient : IDI
    {
        Task SaveAsync(int firmId, int userId, NewAdvanceStatementClientData clientData);

        Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task DeleteByBaseIdAsync(int firmId, int userId, long baseId);

        Task<List<AdvanceStatementInfoDto>> GetListAsync(int firmId, int userId, AdvanceStatementType? type = null, CancellationToken cancellationToken = default);

        Task<AdvanceStatementInfoDto> GetByDocumentBaseIdAsync(int firmId, int userId, long documentBaseId);

        Task<NewAdvanceStatementClientData> GetByIdAsync(int firmId, int userId, long id, HttpQuerySetting setting = null);

        Task<NewAdvanceStatementClientData> GetByBaseIdAsync(int firmId, int userId, long documentBaseId);

        Task<NewAdvanceStatementClientData> GetOrCreateFromBusinessTripAsync(int firmId, int userId, long? baseId,
            long businessTripId);

        Task<DocumentBaseCreateModifyClientData> GetAdvanceStatementCreateModifyClientDataAsync(int firmId, int userId, long documentBaseId, AccountingDocumentType documentType);

        Task<List<AdvanceDocumentClientData>> GetRelatedDocumentsAsync(int firmId, int userId, long documentBaseId);

        /// <summary>
        /// Следует вызвать в процессе объединения номенклатур для обработки мержа АО
        /// </summary>
        Task MergeProductsAsync(int firmId, int userId, ProductMergeRequestDto data);
    }
}