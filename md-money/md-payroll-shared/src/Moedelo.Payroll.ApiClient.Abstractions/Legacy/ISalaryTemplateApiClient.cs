using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalaryTemplates;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy;

public interface ISalaryTemplateApiClient
{
    Task<IReadOnlyCollection<SalaryTemplateDto>> GetByWorkerIdAsync(int firmId, int userId, int workerId,
        CancellationToken token = default);
}