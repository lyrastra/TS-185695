using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Psv;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy;

public interface IPsvReportApiClient
{
    Task<PsvReportResponseDto> GetAsync(int firmId, int userId, int month, int year, int? workerId = null);
}