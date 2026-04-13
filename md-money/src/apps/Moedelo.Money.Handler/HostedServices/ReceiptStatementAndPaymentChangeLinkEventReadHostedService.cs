using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks;
using Moedelo.Money.Business.ReceiptStatements;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices
{
    /// <summary>
    /// Сервис изменяет у связанных с актом приема-передачи 
    /// платежных документов Основного контрагента на прочего
    /// </summary>
    public class ReceiptStatementAndPaymentChangeLinkEventReadHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IReceiptStatementAndPaymentChangeLinkEventsReader eventReader;
        private readonly IReceiptStatementEventProcessor eventProcessor;

        public ReceiptStatementAndPaymentChangeLinkEventReadHostedService(
            ILogger<ReceiptStatementAndPaymentChangeLinkEventReadHostedService> logger,
            IReceiptStatementAndPaymentChangeLinkEventsReader eventReader,
            IReceiptStatementEventProcessor eventProcessor)
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

        private Task OnChangeAsync(ReceiptStatementAndPaymentChangeLinkMessage message)
        {
            var createdLinkIds = message.CreatedLinks
                .Select(x => x.PaymentBaseId)
                .ToArray();

            // нужно исключить из удаленных те связи, которые были пересозданы в том же виде
            var createdLinkIdsToCompare = message.CreatedLinks
                .GroupBy(x => x.PaymentBaseId)
                .ToDictionary(x => x.Key, x => x.Select(l => l.ReceiptStatementBaseId).ToHashSet());
            var deletedLinkIds = message.DeletedLinks
                .Where(x => GetNotRecreatedLinks(x, createdLinkIdsToCompare))
                .Select(x => x.PaymentBaseId)
                .ToArray();

            if (createdLinkIds.Length == 0 && deletedLinkIds.Length == 0)
            {
                return Task.CompletedTask;
            }

            return eventProcessor.ProcessAsync(createdLinkIds, deletedLinkIds);
        }

        private static bool GetNotRecreatedLinks(ReceiptStatementAndPaymentLink x,
            Dictionary<long, HashSet<long>> createdLinkIdsToCompare)
        {
            return !createdLinkIdsToCompare.ContainsKey(x.PaymentBaseId) ||
                   !createdLinkIdsToCompare[x.PaymentBaseId].Contains(x.ReceiptStatementBaseId);
        }
    }
}
