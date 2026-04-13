using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders;
using Moedelo.Money.Business.Abstractions.Operations;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Operations
{
    [InjectAsSingleton(typeof(IOperationsRemover))]
    class OperationsRemover : IOperationsRemover
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IBaseDocumentReader baseDocumentReader;
        private readonly IPaymentOrderRemover paymentOrderRemover;
        private readonly ICashOrderRemover cashOrderRemover;
        private readonly IPurseOperationApiClient purseOperationApiClient;

        public OperationsRemover(
            IExecutionInfoContextAccessor contextAccessor,
            IBaseDocumentReader baseDocumentReader,
            IPaymentOrderRemover paymentOrderRemover,
            ICashOrderRemover cashOrderRemover,
            IPurseOperationApiClient purseOperationApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.baseDocumentReader = baseDocumentReader;
            this.paymentOrderRemover = paymentOrderRemover;
            this.cashOrderRemover = cashOrderRemover;
            this.purseOperationApiClient = purseOperationApiClient;
        }

        public async Task DeleteAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var baseDocuments = await baseDocumentReader.GetByIdsAsync(documentBaseIds);
            if (baseDocuments.Length <= 0)
            {
                return;
            }
            await Task.WhenAll(
                DeletePaymentOrdersAsync(baseDocuments),
                DeleteCashOrdersAsync(baseDocuments),
                DeletePurseOperationsAsync(baseDocuments));
        }

        private async Task DeletePaymentOrdersAsync(IReadOnlyCollection<BaseDocument> baseDocuments)
        {
            var paymentOrderBaseIds = baseDocuments.Where(x => x.Type == LinkedDocumentType.PaymentOrder)
                .Select(x => x.Id)
                .ToArray();
            if (paymentOrderBaseIds.Length <= 0)
            {
                return;
            }
            await paymentOrderRemover.DeleteAsync(paymentOrderBaseIds);
        }

        private async Task DeleteCashOrdersAsync(IReadOnlyCollection<BaseDocument> baseDocuments)
        {
            var cashOrderBaseIds = baseDocuments.Where(x => x.Type == LinkedDocumentType.IncomingCashOrder ||
                                                            x.Type == LinkedDocumentType.OutcomingCashOrder)
                .Select(x => x.Id)
                .ToArray();
            if (cashOrderBaseIds.Length <= 0)
            {
                return;
            }
            await cashOrderRemover.DeleteAsync(cashOrderBaseIds);
        }

        private async Task DeletePurseOperationsAsync(IReadOnlyCollection<BaseDocument> baseDocuments)
        {
            var purseOperationBaseIds = baseDocuments.Where(x => x.Type == LinkedDocumentType.PurseOperation)
                .Select(x => x.Id)
                .ToArray();
            if (purseOperationBaseIds.Length <= 0)
            {
                return;
            }
            var context = contextAccessor.ExecutionInfoContext;
            await purseOperationApiClient.DeleteAsync(context.FirmId, context.UserId, purseOperationBaseIds);
        }
    }
}
