using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.File;
using Moedelo.Payroll.Shared.Enums.Common;

namespace Moedelo.Payroll.ApiClient.Abstractions.Ndfl2IncomeStatement;

public interface INdfl2FileApiClient
{
    Task<FileResultDto> GetFileAsync(FirmId firmId, UserId userId, int year, int workerId, int? number,
        DocumentFileType fileFormat, CancellationToken token = default);
}