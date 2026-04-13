using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Interfaces.Business.Money.MoneyTransfers;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.MoneyTransfers;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Services.Money.Operations.MoneyTransfers
{
    [InjectAsSingleton]
    public class MoneyTransfersOperationReader : IMoneyTransfersOperationReader
    {
        private readonly IMoneyTransferOperationDao moneyTransferOperationDao;

        public MoneyTransfersOperationReader(IMoneyTransferOperationDao moneyTransferOperationDao)
        {
            this.moneyTransferOperationDao = moneyTransferOperationDao;
        }

        public Task<List<MoneyTransferOperation>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds)
        {
            return moneyTransferOperationDao.GetByBaseIdsAsync(firmId, baseIds);
        }
    }
}
