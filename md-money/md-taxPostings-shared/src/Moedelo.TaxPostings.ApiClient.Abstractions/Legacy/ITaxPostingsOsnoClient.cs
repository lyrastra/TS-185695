using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.TaxPostings.ApiClient.Abstractions.Legacy
{
    public interface ITaxPostingsOsnoClient
    {
        Task<List<TaxPostingOsnoDto>> GetByBaseIdAsync(FirmId firmId, UserId userId, long baseId);

        Task SaveAsync(FirmId firmId, UserId userId, IReadOnlyCollection<TaxPostingOsnoDto> taxPostings);

        Task DeleteAsync(FirmId firmId, UserId userId, long baseId);
    }
}