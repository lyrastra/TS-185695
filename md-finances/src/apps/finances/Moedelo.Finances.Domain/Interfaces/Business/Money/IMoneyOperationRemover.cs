using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IMoneyOperationRemover : IDI
    {
        Task DeleteByIdAsync(int firmId, int userId, long documentBaseId);

        Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);
    }
}