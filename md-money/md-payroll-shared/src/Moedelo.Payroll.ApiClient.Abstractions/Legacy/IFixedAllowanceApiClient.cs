using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FixedAllowance;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy;

public interface IFixedAllowanceApiClient
{
    Task<IReadOnlyCollection<FixedAllowanceDto>> GetAllAsync(CancellationToken token = default);
}