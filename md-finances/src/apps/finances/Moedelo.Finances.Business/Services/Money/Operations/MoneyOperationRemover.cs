using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.AccountingV2.Client.PaymentOrder;
using Moedelo.AccountingV2.Client.CashOrder;
using Moedelo.AccountingV2.Client.PurseOperations;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;

namespace Moedelo.Finances.Business.Services.Money.Operations
{
    [InjectAsSingleton]
    public class MoneyOperationRemover : IMoneyOperationRemover
    {
        private const string TAG = nameof(MoneyOperationRemover);

        private readonly ILogger logger;
        private readonly IPaymentOrderApiClient paymentOrderApiClient;
        private readonly ICashOrderApiClient cashOrderApiClient;
        private readonly IPurseOperationApiClient purseOperationApiClient;
        private readonly IPaymentOrderOperationRemover paymentOrderOperationRemover;
        private readonly IOperationDao operationDao;

        public MoneyOperationRemover(
            ILogger logger,
            IPaymentOrderApiClient paymentOrderApiClient,
            ICashOrderApiClient cashOrderApiClient,
            IPurseOperationApiClient purseOperationApiClient,
            IPaymentOrderOperationRemover paymentOrderOperationRemover,
            IOperationDao operationDao)
        {
            this.logger = logger;
            this.paymentOrderApiClient = paymentOrderApiClient;
            this.cashOrderApiClient = cashOrderApiClient;
            this.purseOperationApiClient = purseOperationApiClient;
            this.paymentOrderOperationRemover = paymentOrderOperationRemover;
            this.operationDao = operationDao;
        }

        public async Task DeleteByIdAsync(int firmId, int userId, long documentBaseId)
        {
            var operationKind = await operationDao.GetKindByBaseIdAsync(firmId, documentBaseId).ConfigureAwait(false);
            if (operationKind == null)
            {
                return;
            }
            await DeleteOperationsByKindAsync(firmId, userId, operationKind.Value, new[] { documentBaseId }).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            var operationBaseIdsByKinds = (await operationDao.GetKindsByBaseIdsAsync(firmId, documentBaseIds).ConfigureAwait(false))
                .GroupBy(x => x.OperationKind);
            foreach (var operationBaseIdsByKind in operationBaseIdsByKinds)
            {
                try
                {
                    var operationKind = operationBaseIdsByKind.Key;
                    var baseIds = operationBaseIdsByKind.Select(x => x.DocumentBaseId).ToList();
                    await DeleteOperationsByKindAsync(firmId, userId, operationKind, baseIds).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    logger.Error(TAG, ex.Message, ex);
                }
            }
        }

        private async Task DeleteOperationsByKindAsync(int firmId, int userId, OperationKind operationKind, IReadOnlyCollection<long> baseIds)
        {
            switch (operationKind)
            {
                case OperationKind.PaymentOrderOperation:
                    await paymentOrderOperationRemover.DeleteByBaseIdsAsync(firmId, userId, baseIds).ConfigureAwait(false);
                    break;
                case OperationKind.CashOrderOperation:
                    await cashOrderApiClient.DeleteAsync(firmId, userId, baseIds).ConfigureAwait(false);
                    break;
                case OperationKind.PurseOperation:
                    await purseOperationApiClient.DeleteAsync(firmId, userId, baseIds).ConfigureAwait(false);
                    break;
            }
        }
    }
}