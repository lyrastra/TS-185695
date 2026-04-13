using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.MoneyStatistics;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.MoneyStatistics
{
    public interface IIncomeApiClient : IDI
    {
        /// <summary>
        /// Считает доходы по указанным фирмам за текущий год
        /// </summary>
        /// <param name="requestDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<FirmIncomeDto>> CalculateByFirmIdsAsync(ExecuteByFirmIdsRequestDto requestDto, CancellationToken cancellationToken);
    }
}