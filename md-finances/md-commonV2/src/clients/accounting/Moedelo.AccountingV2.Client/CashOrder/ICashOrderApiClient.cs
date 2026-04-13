using Moedelo.AccountingV2.Dto.CashOrder;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccountingV2.Client.CashOrder
{
    public interface ICashOrderApiClient : IDI
    {
        Task<List<FirmCashOrderDto>> GetListAsync(int firmId, int userId, CashOrderPaginationRequest paginationRequest,
            CancellationToken cancellationToken = default);

        Task<List<FirmCashOrderDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds,
            CancellationToken cancellationToken = default);

        Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, HttpQuerySetting setting = default);

        Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task<long> Save(int firmId, int userId, BudgetaryCashOrderDto dto);
    }
}