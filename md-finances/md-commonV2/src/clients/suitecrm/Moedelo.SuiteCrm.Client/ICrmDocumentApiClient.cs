using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.SuiteCrm.Client
{
    public interface ICrmDocumentApiClient : IDI
    {
        Task GetDocumentFileAsync(string documentId);
        Task GetDocumentPreviewFileAsync(string documentId, int pageIndex);
        Task CreateDocumentPreviewAsync(string documentId);
    }
}