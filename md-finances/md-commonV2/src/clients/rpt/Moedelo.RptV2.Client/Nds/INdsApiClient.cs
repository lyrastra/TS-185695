using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Client.Nds.Request;

namespace Moedelo.RptV2.Client.Nds
{
    public interface INdsApiClient : IDI
    {
        Task<FileResponse> GetAsync(InventoryJournalOfInvoicesXmlRequest request);
    }
}