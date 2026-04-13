using Moedelo.Documents.Dto.DocumentTypes;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Documents.Client.DocumentTypes
{
    public interface IDocumentTypeApiClient : IDI
    {
        Task<DocumentTypeDto> CreateAsync(DocumentTypePostDto dto);
        Task<DocumentTypeDto> GetAsync(int accountId, int id);
    }
}