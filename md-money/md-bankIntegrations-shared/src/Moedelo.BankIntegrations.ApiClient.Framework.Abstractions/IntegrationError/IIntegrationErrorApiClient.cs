using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationError;

public interface IIntegrationErrorApiClient
{
    Task MergeFromGetStatementResultAsync(MergeFromGetStatementErrorRequestDto dto);
    Task MergeFromErrorStringAsync(MergeFromErrorIntegrationErrorRequestDto dto);
    Task ReadUnreadByIdsAsync(ReadUnreadIntegrationErrorRequestDto dto);
    Task<IntegrationErrorListDto> GetListAsync(GetListIntegrationErrorRequestDto dto, CancellationToken ctx = default);
}