using Moedelo.CatalogV2.Dto.NdsDeclarationSection7Code;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.CatalogV2.Client.NdsDeclarationSection7Code
{
    public interface INdsDeclarationSection7CodeApiClient : IDI
    {
        Task<List<NdsDeclarationSection7CodeDto>> GetByIds(IReadOnlyCollection<int> ids);
        Task<List<NdsDeclarationSection7CodeDto>> GetByCodes(IReadOnlyCollection<string> codes);
    }
}
