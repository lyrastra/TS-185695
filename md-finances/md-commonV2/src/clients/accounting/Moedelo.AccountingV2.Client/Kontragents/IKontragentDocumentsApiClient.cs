using System.Threading.Tasks;
using Moedelo.AccountingV2.Client.Kontragents.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccountingV2.Client.Kontragents
{
    public interface IKontragentDocumentsApiClient : IDI
    {
        Task<HttpFileModel> DownloadReconcillationStatementAsync(int firmId, int userId, ReconcillationStatementRequestDto dto);
    }
}