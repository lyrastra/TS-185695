using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.Money.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Operations.PaymentOrders
{
    /// <inheritdoc cref="IPaymentOrderOperationRemover"/>
    [InjectAsSingleton]
    public class PaymentOrderOperationRemover : IPaymentOrderOperationRemover
    {
        private readonly IMoneyOperationsClient moneyOperationsClient;
        private readonly IPaymentOrderOperationDao dao;

        public PaymentOrderOperationRemover(
            IMoneyOperationsClient moneyOperationsClient,
            IPaymentOrderOperationDao dao)
        {
            this.moneyOperationsClient = moneyOperationsClient;
            this.dao = dao;
        }

        public async Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            const int batchSize = 500;
            var batchList = baseIds.Select((x, i) => new { Id = x, Index = i })
                .GroupBy(x => x.Index / batchSize, x => x.Id);
            foreach (var batch in batchList)
            {
                var idsToDelete = batch.ToList();
                await DeleteInternalAsync(firmId, userId, idsToDelete).ConfigureAwait(false);
            }
        }

        public async Task DeleteUncategorizedAsync(int firmId, int userId, long? sourceId = null)
        {
            var idsToDelete = await dao.GetBaseIdsForUncategorizedAsync(firmId, sourceId).ConfigureAwait(false);
            await DeleteInternalAsync(firmId, userId, idsToDelete).ConfigureAwait(false);
        }

        private async Task DeleteInternalAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            var duplicateBaseIds = await dao.GetDuplicateBaseIdsAsync(firmId, baseIds).ConfigureAwait(false);
            if (duplicateBaseIds?.Any() ?? false)
            {
                await moneyOperationsClient.DeleteAsync(firmId, userId, duplicateBaseIds.AsReadOnly())
                    .ConfigureAwait(false);
            }
            await moneyOperationsClient.DeleteAsync(firmId, userId, baseIds).ConfigureAwait(false);
        }
    }
}