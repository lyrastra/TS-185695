using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.Upd
{
    public interface IUpdProvideApiClient : IDI
    {
        Task ReprovideByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}