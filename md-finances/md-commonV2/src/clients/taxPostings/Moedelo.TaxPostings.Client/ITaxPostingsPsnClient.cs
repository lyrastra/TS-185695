using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.TaxPostings.Dto;

namespace Moedelo.TaxPostings.Client
{
    public interface ITaxPostingsPsnClient : IDI
    {
        Task<List<TaxPostingPsnDto>> GetByBaseIdAsync(int firmId, int userId, long documentBaseId);

        Task<List<TaxPostingPsnDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        Task<List<TaxPostingPsnDto>> GetByRelatedDocumentAsync(int firmId, int userId, long documentBaseId);

        Task SaveAsync(int firmId, int userId, IReadOnlyCollection<TaxPostingPsnDto> taxPostings);

        Task DeleteAsync(int firmId, int userId, long documentBaseId);

        Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        Task<List<TaxPostingPsnDto>> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<List<DocumentTaxSumDto>> GetDocumentTaxSumsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        Task<bool> HasPostingsByAnyDocumentAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);
    }
}