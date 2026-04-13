using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IDocumentLoadApiClient
    {
        Task<HttpFileModel> DownloadJobStatementAsync(FirmId firmId, UserId userId, int workerId, CancellationToken ct);

        Task<HttpFileModel> DownloadPersonalOrderAsync(FirmId firmId, UserId userId, int workerId, CancellationToken ct);

        Task<HttpFileModel> DownloadWorkContractAsync(FirmId firmId, UserId userId, int workerId, CancellationToken ct);
    }
}
