using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.TaxPostings.ApiClient.Abstractions.Legacy
{
    public interface ITaxPostingsPsnClient
    {
        Task<TaxPostingPsnDto[]> GetByBaseIdAsync(FirmId firmId, UserId userId, long documentBaseId);

        Task<TaxPostingPsnDto[]> GetByRelatedDocumentAsync(FirmId firmId, UserId userId, long documentBaseId);

        Task SaveAsync(FirmId firmId, UserId userId, IReadOnlyCollection<TaxPostingPsnDto> taxPostings);

        Task DeleteByRelatedDocumentAsync(FirmId firmId, UserId userId, long documentBaseId);

        Task<TaxPostingsPsnByFirmDto[]> GetByPeriodAndFirmIdsAsync(TaxPostingsQuery query, HttpQuerySetting setting = null);
    }
}
