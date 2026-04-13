using System.Threading.Tasks;
using Moedelo.Finances.Client.Money.Dtos;
using Moedelo.Finances.Dto.Money;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Client.Money
{
    public interface IMoneyReconciliationClient : IDI
    {
        Task ReconcileForBackofficeAsync(int firmId, int userId, ReconciliationForBackofficeRequestDto request);

        Task ReconcileForUserAsync(int firmId, int userId, ReconciliationForUserRequestDto request);

        Task<ReconciliationResponseDto> GetLastAsync(int firmId, int userId, int settlementAccountId);
    }
}
