using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Contracts
{
    internal interface IContractsReader
    {
        Task<Contract> GetByBaseIdAsync(long baseId);
        Task<IReadOnlyCollection<Contract>> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds);
        Task<Contract> GetMainContractAsync(int kontragentId);
    }
}
