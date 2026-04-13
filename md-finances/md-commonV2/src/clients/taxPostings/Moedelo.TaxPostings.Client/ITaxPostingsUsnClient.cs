using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.TaxPostings.Dto;

namespace Moedelo.TaxPostings.Client
{
    public interface ITaxPostingsUsnClient : IDI
    {
        [Obsolete("use DeleteByRelatedDocumentAsync")]
        Task DeleteAsync(int firmId, int userId, long documentBaseId);
        
        Task DeleteByRelatedDocumentsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        Task DeleteByRelatedDocumentAsync(int firmId, int userId, long documentBaseId);

        Task<List<DocumentTaxSumDto>> GetDocumentTaxSumsAsync(int firmId, int userId,
            IReadOnlyCollection<long> baseIdList);

        Task<List<TaxPostingUsnDto>> GetByDocumentIdsAsync(int firmId, int userId,
            IReadOnlyCollection<long> documentBaseIds);

        Task<List<TaxPostingUsnDto>> GetByPeriodsAsync(int firmId, int userId,
            IReadOnlyCollection<PeriodRequestDto> periods);

        Task SaveAsync(int firmId, int userId, IReadOnlyCollection<TaxPostingUsnDto> taxPostings);
    }
}