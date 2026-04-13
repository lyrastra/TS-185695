using System.Threading.Tasks;
using Moedelo.Finances.Dto.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Client.Money
{
    public interface IMoneyIncomingBalanceOperationClient : IDI
    {
        Task<MoneyIncomingBalanceOperationDto> GetAsync(int firmId, int userId, int settlementAccountId);
        Task SaveAsync(int firmId, int userId, MoneyIncomingBalanceOperationDto operation);
        Task DeleteAsync(int firmId, int userId, int settlementAccountId);
    }
}