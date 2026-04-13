using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// source https://github.com/moedelo/md-commonV2/blob/29552634b941a28f95a5250b987de1f9d3dd974b/src/clients/catalog/Moedelo.CatalogV2.Client/NdsDeclarationSection7Code/INdsDeclarationSection7CodeApiClient.cs#L8
    /// </summary>
    public interface INdsDeclarationSection7CodeApiClient
    {
        Task<List<NdsDeclarationSection7CodeDto>> GetByIds(IReadOnlyCollection<int> ids);
        Task<List<NdsDeclarationSection7CodeDto>> GetByCodes(IReadOnlyCollection<string> codes);
    }
}