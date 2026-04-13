using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.Postings.Client.LinkedDocument;

namespace Moedelo.Finances.Business.Services.Money.Operations.PaymentOrders
{
    [InjectAsSingleton(typeof(IMoneyTransfersOperationRemover))]
    public class MoneyTransfersOperationRemover : IMoneyTransfersOperationRemover
    {
        private readonly ILinkedDocumentClient linkedDocumentClient;
        private readonly IMoneyTransferOperationDao dao;

        public MoneyTransfersOperationRemover(
            ILinkedDocumentClient linkedDocumentClient,
            IMoneyTransferOperationDao dao)
        {
            this.linkedDocumentClient = linkedDocumentClient;
            this.dao = dao;
        }

        public async Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            //todo: delete related entities
            await dao.DeleteByBaseIdsAsync(firmId, baseIds).ConfigureAwait(false);
            await baseIds.RunParallelAsync(baseId => linkedDocumentClient.DeleteAsync(baseId, firmId, userId));
        }
    }
}