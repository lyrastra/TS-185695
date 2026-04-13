using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.LogService;

namespace Moedelo.BankIntegrations.ApiClient.LogService;

public interface IRequestResponseLogSaverApiClient
{
    Task<string> SaveRequestResponseLogAsync(
        SaveRequestResponseLogRequestDto request,
        CancellationToken cancellationToken = default);
}
