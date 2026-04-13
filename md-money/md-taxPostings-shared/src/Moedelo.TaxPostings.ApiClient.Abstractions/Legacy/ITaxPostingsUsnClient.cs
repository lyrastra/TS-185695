using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.TaxPostings.ApiClient.Abstractions.Legacy
{
    public interface ITaxPostingsUsnClient
    {
        Task<TaxPostingUsnDto[]> GetByDocumentIdAsync(FirmId firmId, UserId userId, long documentBaseId);

        Task<TaxPostingUsnDto[]> GetByDocumentIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds);

        Task<TaxPostingUsnDto[]> GetByPeriodAsync(FirmId firmId, UserId userId, PeriodRequestDto period);

        Task<TaxPostingUsnByFirmDto[]> GetByPeriodAndFirmIdsAsync(IReadOnlyCollection<PeriodRequestByFirmDto> periodByFirmsDto, 
            HttpQuerySetting setting = null);

        Task SaveAsync(FirmId firmId, UserId userId, IReadOnlyCollection<TaxPostingUsnDto> taxPostings);

        Task DeleteAsync(FirmId firmId, UserId userId, long documentBaseId);

        Task DeleteByRelatedDocumentIdAsync(FirmId firmId, UserId userId, long documentBaseId);
        
        Task DeleteByRelatedDocumentsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds);

        /// <summary>
        /// Удаляет проводки УСН по связям НУ с <paramref name="documentBaseId"/>, кроме проводок «на самом» этом документе
        /// (<c>PostingForTax.DocumentId == documentBaseId</c>).
        /// </summary>
        Task DeleteByRelatedDocumentIdNotInDocumentIdAsync(
            FirmId firmId,
            UserId userId,
            long documentBaseId);
    }
}