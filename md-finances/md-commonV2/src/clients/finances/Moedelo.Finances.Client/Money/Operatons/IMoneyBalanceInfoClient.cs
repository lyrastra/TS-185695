using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Client.Money
{
    public interface IMoneyBalanceInfoClient : IDI
    {
        Task<Dictionary<int, bool>> GetIsBalanceSettedForFirmsAsync(List<int> firmIds);
    }
}