using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.File;

namespace Moedelo.Payroll.ApiClient.Abstractions.Payslip;

public interface IPayslipFileApiClient
{
    Task<FileResultDto> GetFileAsync(FirmId firmId, UserId userId, int year, int month, int workerId,
        CancellationToken token = default);
}