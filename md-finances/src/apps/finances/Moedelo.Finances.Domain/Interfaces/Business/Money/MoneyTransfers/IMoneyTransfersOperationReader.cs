using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Money.Operations.MoneyTransfers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.MoneyTransfers
{
    public interface IMoneyTransfersOperationReader : IDI
    {
        Task<List<MoneyTransferOperation>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);
    }
}
