using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Finances.Domain.Models.Money.Operations;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IMoneyOperationLinkedDocumentsReader : IDI
    {
        Task<List<LinkedDocument>> GetByBaseIdAsync(IUserContext userContext, long baseId);
        Task<Dictionary<long, List<LinkedDocument>>> GetMapByBaseIdsAsync(IUserContext userContext, IReadOnlyCollection<long> baseIds);
    }
}
