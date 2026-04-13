using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using System.Linq;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.LinkedDocuments.Links
{
    [InjectAsSingleton(typeof(IPurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventProcessor))]
    internal class PurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventProcessor : IPurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventProcessor
    {
        private readonly IBaseDocumentReader baseDocumentReader;
        private readonly BudgetaryPaymentApiClient budgetaryPaymentApiClient;
        private readonly IUnifiedBudgetaryPaymentApiClient unifiedBudgetaryPaymentApiClient;
        private readonly IBudgetaryPaymentProvider provider;
        private readonly IUnifiedBudgetaryPaymentProvider unifiedBudgetaryPaymentProvider;

        public PurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventProcessor(
            IBaseDocumentReader baseDocumentReader,
            BudgetaryPaymentApiClient budgetaryPaymentApiClient,
            IUnifiedBudgetaryPaymentApiClient unifiedBudgetaryPaymentApiClient,
            IBudgetaryPaymentProvider provider,
            IUnifiedBudgetaryPaymentProvider unifiedBudgetaryPaymentProvider)
        {
            this.baseDocumentReader = baseDocumentReader;
            this.budgetaryPaymentApiClient = budgetaryPaymentApiClient;
            this.unifiedBudgetaryPaymentApiClient = unifiedBudgetaryPaymentApiClient;
            this.provider = provider;
            this.unifiedBudgetaryPaymentProvider = unifiedBudgetaryPaymentProvider;
        }

        public Task ProcessAsync(IReadOnlyCollection<long> paymentBaseIds)
        {
            return paymentBaseIds?.RunParallelAsync(ProvideAsync) ?? Task.CompletedTask;
        }

        private async Task ProvideAsync(long paymentBaseId)
        {
            // Внимание! Т. к. функционал доступен только для ИП, здесь опущена валидация на закрытый период (для организации она нужна)

            var baseDocuments = await baseDocumentReader.GetByIdsAsync(new[] { paymentBaseId });
            var baseDocument = baseDocuments.FirstOrDefault();

            if (baseDocument == null)
            {
                return;
            }

            if (baseDocument.Type == LinkedDocumentType.PaymentOrder)
            {
                try
                {
                    // при изменении связей с валютными инвойсами скидываем НУ в автоматический режим
                    await budgetaryPaymentApiClient.UpdateTaxPostingsModeAsync(paymentBaseId, false);
                    await provider.ProvideAsync(paymentBaseId);
                }
                catch (OperationNotFoundException)
                {
                    // пропускаем не найденные платежи
                }
            }

            if (baseDocument.Type == LinkedDocumentType.UnifiedBudgetarySubPayment)
            {
                try
                {
                    var parentBaseId = await unifiedBudgetaryPaymentApiClient.GetSubPaymentParentIdAsync(paymentBaseId);
                    // при изменении связей с валютными инвойсами скидываем НУ дочернего платежа в автоматический режим
                    await unifiedBudgetaryPaymentApiClient.UpdateTaxPostingTypeAsync(paymentBaseId, ProvidePostingType.Auto);
                    await unifiedBudgetaryPaymentProvider.ProvideAsync(parentBaseId);
                }
                catch (OperationNotFoundException)
                {
                    // пропускаем не найденные дочерние платежи
                }
            }
        }
    }
}