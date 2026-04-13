using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Client
{
    public interface IMoneyOperationsClient : IDI
    {
        Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);
    }
}