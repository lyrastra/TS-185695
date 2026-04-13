using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalaryYears;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy;

public interface ISalaryYearApiClient
{
    Task<IReadOnlyCollection<SalaryYearDto>> GetAllAsync(CancellationToken token = default);
}