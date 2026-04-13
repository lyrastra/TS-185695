using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.HistoricalLogsV2.Dto;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.HistoricalLogsV2.Client;

public interface IOperationLogWriteApiClient
{
    Task LogAsync(LogOperationDto data, HttpQuerySetting querySetting = default, CancellationToken cancellationToken = default);
    Task LogAsync(IReadOnlyCollection<LogOperationDto> records, HttpQuerySetting querySetting = default, CancellationToken cancellationToken = default);
}
