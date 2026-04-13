using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.RsvReportInitialData;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy;

public interface IRsvReportInitialDataApiClient
{
    Task<RsvReportInitialDataDto> GetInitialDataAsync(FirmId firmId, UserId userId, int year, int periodNumber);
}