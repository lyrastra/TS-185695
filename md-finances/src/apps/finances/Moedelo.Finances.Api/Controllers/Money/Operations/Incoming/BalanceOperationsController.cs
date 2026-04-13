using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;

namespace Moedelo.Finances.Api.Controllers.Money.Operations.Incoming
{
    [RoutePrefix("Money/Operations/Incoming/BalanceOperations")]
    public class BalanceOperationsController : ApiController
    {
        private readonly IMoneyTransferOperationDao moneyTransferOperationDao;

        public BalanceOperationsController(IMoneyTransferOperationDao moneyTransferOperationDao)
        {
            this.moneyTransferOperationDao = moneyTransferOperationDao;
        }

        [HttpPost]
        [Route("GetIsBalanceSettedForFirms")]
        public Task<Dictionary<int, bool>> GetIsBalanceSettedForFirms(IReadOnlyCollection<int> firmIds)
        {
            return moneyTransferOperationDao.GetIsBalanceSettedForFirmsAsync(firmIds);
        }
    }
}