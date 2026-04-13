using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices
{
    /// <summary>
    /// Сервис реагирует на изменение связей инвойсов (покупки) и бюджетных платежей: перепроводиn НУ для БП
    /// </summary>
    public class PurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventReadHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IPurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventsReader eventReader;
        private readonly IPurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventProcessor eventProcessor;

        public PurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventReadHostedService(
            ILogger<PurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventReadHostedService> logger,
            IPurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventsReader eventReader,
            IPurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventProcessor eventProcessor)
        {
            this.logger = logger;
            this.eventReader = eventReader;
            this.eventProcessor = eventProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReader.ReadAsync(MoneyConstants.GroupId, OnChangeAsync, cancellationToken: cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnChangeAsync(PurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkMessage message)
        {
            var paymentBaseIds = (message.CreatedLinks?.Select(x => x.PaymentBaseId) ?? Array.Empty<long>())
                .Concat(message.DeletedLinks?.Select(x => x.PaymentBaseId) ?? Array.Empty<long>())
                .ToHashSet();

            return eventProcessor.ProcessAsync(paymentBaseIds);
        }
    }
}
