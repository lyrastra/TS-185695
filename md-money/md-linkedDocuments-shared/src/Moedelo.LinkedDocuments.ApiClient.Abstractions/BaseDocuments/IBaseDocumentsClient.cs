using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments
{
    public interface IBaseDocumentsClient
    {
        Task<BaseDocumentDto> GetByIdAsync(long id);
        
        Task<BaseDocumentDto[]> GetByIdsAsync(IReadOnlyCollection<long> ids);
        
        Task<long> CreateAsync(BaseDocumentCreateDto dto);

        Task UpdateAsync(BaseDocumentUpdateDto dto);
    }
}