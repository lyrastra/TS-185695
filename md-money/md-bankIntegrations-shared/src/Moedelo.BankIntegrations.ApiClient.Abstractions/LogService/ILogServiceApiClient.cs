using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.LogService;

namespace Moedelo.BankIntegrations.ApiClient.LogService;

public interface ILogServiceApiClient
{
    Task<string> AppendAsync(AppendLogRequestDto requestDto, CancellationToken cancellationToken);
}
