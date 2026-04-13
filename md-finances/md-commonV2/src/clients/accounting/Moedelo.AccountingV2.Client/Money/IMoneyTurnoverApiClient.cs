using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Money;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Money
{
    public interface IMoneyTurnoverApiClient : IDI
    {
        Task<List<MoneyTurnoverDto>> GetByContractIdsAsync(int firmId, int userId, IReadOnlyCollection<int> contractIds);

        Task<List<MoneyTurnoverDto>> GetByKontragentIdsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);
    }
}
