using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models;
using Moedelo.Finances.Domain.Models.Kontragents;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Operations
{
    [InjectAsSingleton]
    public class MoneyOperationReader : IMoneyOperationReader
    {
        private readonly IOperationDao dao;
        private readonly IPaymentOrderOperationDao paymentOrderOperationDao;
        private readonly IMoneyTransferOperationDao moneyTransferOperationDao;

        public MoneyOperationReader(
            IOperationDao dao,
            IPaymentOrderOperationDao paymentOrderOperationDao,
            IMoneyTransferOperationDao moneyTransferOperationDao)
        {
            this.dao = dao;
            this.paymentOrderOperationDao = paymentOrderOperationDao;
            this.moneyTransferOperationDao = moneyTransferOperationDao;
        }

        public Task<DateTime?> GetLastOperationDateUntilAsync(int firmId, DateTime date)
        {
            return paymentOrderOperationDao.GetLastDateUntilAsync(firmId, date);
        }

        public async Task<List<KontragentTurnover>> TopByOperationsWithKontragentsAsync(IUserContext userContext, int count, DateTime startDate, DateTime endDate)
        {
            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);
            if (isBiz)
            {
                return await moneyTransferOperationDao.TopByOperationsWithKontragentsAsync(userContext.FirmId, count, startDate, endDate).ConfigureAwait(false);
            }
            return await dao.TopByOperationsWithKontragentsAsync(userContext.FirmId, count, startDate, endDate).ConfigureAwait(false);
        }

        public async Task<long> GetDocumentBaseIdAsync(IUserContext userContext, long id)
        {
            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);
            if (isBiz)
            {
                throw new NotImplementedException();
            }
            var list = await dao.GetOperationsKindsByIdsAsync(userContext.FirmId, new[] { id }).ConfigureAwait(false);
            return list.Select(x => x.DocumentBaseId).FirstOrDefault();
        }

        public async Task<List<PaymentOrderStatus>> GetStatusesByBaseIdsAsync(IUserContext userContext, IReadOnlyCollection<long> baseIds)
        {
            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);
            if (isBiz)
            {
                throw new NotImplementedException();
            }
            return await paymentOrderOperationDao.GetStatusesByBaseIdsAsync(userContext.FirmId, baseIds).ConfigureAwait(false);
        }

        public async Task<bool> HasOperationsBySettlementAccountAsync(IUserContext userContext, int settlementAccountId)
        {
            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);
            if (isBiz)
            {
                return await moneyTransferOperationDao.HasOperationsBySettlementAccountAsync(userContext.FirmId, settlementAccountId).ConfigureAwait(false);
            }
            return await paymentOrderOperationDao.HasOperationsBySettlementAccountAsync(userContext.FirmId, settlementAccountId).ConfigureAwait(false);
        }

        public Task<List<OperationKindAndBaseId>> GetKindsByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds)
        {
            return dao.GetKindsByBaseIdsAsync(firmId, baseIds);
        }

        public Task<MoneyOperation> GetByBaseIdAsync(int firmId, long baseId)
        {
            return dao.GetByBaseIdAsync(firmId, baseId);
        }

        public Task<MoneyOperation[]> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds)
        {
            return dao.GetByBaseIdsAsync(firmId, baseIds);
        }

        public Task<MoneyOperation[]> GetByPeriodAsync(int firmId, Period period)
        {
            return dao.GetByPeriodAsync(firmId, period);
        }

        public Task<MoneyOperation[]> GetByPatentAsync(int firmId, long patentId)
        {
            return dao.GetByPatentAsync(firmId, patentId);
        }
    }
}
