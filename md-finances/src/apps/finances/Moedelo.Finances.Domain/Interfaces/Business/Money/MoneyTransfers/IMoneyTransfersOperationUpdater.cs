using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Money.Operations.MoneyTransfers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.MoneyTransfers
{
    public interface IMoneyTransfersOperationUpdater : IDI
    {
        Task SaveAsync(int firmId, int userId, MoneyTransferOperation operation);
    }
}
