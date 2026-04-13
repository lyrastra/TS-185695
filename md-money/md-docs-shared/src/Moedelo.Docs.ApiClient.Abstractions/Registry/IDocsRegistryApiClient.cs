using Moedelo.Docs.ApiClient.Abstractions.Registry.Models;
using System.Threading.Tasks;

namespace Moedelo.Docs.ApiClient.Abstractions.Registry
{
    public interface IDocsRegistryApiClient
    {
        Task<DocumentsResponseDto> GetAsync(DocumentsRequestDto requestDto);
    }
}