using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing;

namespace Moedelo.Money.Business.ReceiptStatements
{
    [InjectAsSingleton(typeof(IReceiptStatementEventProcessor))]
    internal sealed class ReceiptStatementEventProcessor : IReceiptStatementEventProcessor
    {
        private readonly IBaseDocumentReader baseDocumentReader;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ILinkedPaymentOrderUpdater paymentOrderUpdater;
        private readonly ICashApiClient cashApiClient;
        private readonly IPaymentOrderProvider paymentOrderProvider;

        public ReceiptStatementEventProcessor(
            IBaseDocumentReader baseDocumentReader,
            IExecutionInfoContextAccessor contextAccessor,
            ILinkedPaymentOrderUpdater paymentOrderUpdater,
            ICashApiClient cashApiClient,
            IPaymentOrderProvider paymentOrderProvider)
        {
            this.baseDocumentReader = baseDocumentReader;
            this.contextAccessor = contextAccessor;
            this.paymentOrderUpdater = paymentOrderUpdater;
            this.cashApiClient = cashApiClient;
            this.paymentOrderProvider = paymentOrderProvider;
        }

        public async Task ProcessAsync(IReadOnlyCollection<long> createdLinkIds, IReadOnlyCollection<long> deletedLinkIds)
        {
            if (createdLinkIds.Count > 0)
            {
                await ChangeMainContractorAsync(createdLinkIds);
            }

            if (deletedLinkIds.Count > 0)
            {
                // только п/п, вероятно надо еще для кассы
                await deletedLinkIds.RunParallelAsync(paymentOrderProvider.ProvideAsync);
            }
        }

        private async Task ChangeMainContractorAsync(IReadOnlyCollection<long> createdLinkIds)
        {
            var baseDocs = await baseDocumentReader.GetByIdsAsync(createdLinkIds);
            var paymentOrderIds = GetOrderIds(baseDocs, LinkedDocumentType.PaymentOrder);
            var cashOrderIds = GetOrderIds(baseDocs, LinkedDocumentType.OutcomingCashOrder);

            var paymentUpdateTask = paymentOrderUpdater.UpdatePaymentsAsync(paymentOrderIds);

            var context = contextAccessor.ExecutionInfoContext;
            var cashUpdateTask = cashApiClient.SetOtherKontragent(context.FirmId, context.UserId, cashOrderIds);

            await Task.WhenAll(paymentUpdateTask, cashUpdateTask);
        }

        private static long[] GetOrderIds(IEnumerable<BaseDocument> baseDocs, LinkedDocumentType type)
        {
            return baseDocs
                .Where(x => x.Type == type)
                .Select(x => x.Id)
                .ToArray();
        }
    }
}
