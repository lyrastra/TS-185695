using Moedelo.TaxPostings.Dto.Postings.Dto;
using System.Threading.Tasks;

namespace Moedelo.TaxPostings.Client.Postings
{
    public interface ITaxPostingsApiClient
    {
        Task<ITaxPostingsResponseDto<ITaxPostingDto>> GetByDocumentIdAsync(int firmId, int userId, long baseId);
    }
}