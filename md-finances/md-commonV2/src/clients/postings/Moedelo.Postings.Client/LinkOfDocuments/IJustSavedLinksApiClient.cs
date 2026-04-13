using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Postings.Dto;

namespace Moedelo.Postings.Client.LinkOfDocuments
{
    public interface IJustSavedLinksApiClient
    {
        Task SaveAsync(long baseId, List<RelationWithDto> links);
        Task<List<RelationWithDto>> GetAsync(long baseId);
        Task<List<LinkOfDocumentsDto>> GetAsLinkOfDocumentsAsync(int firmId, long baseId);
    }
}