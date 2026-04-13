using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Import;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Import
{
    [InjectAsSingleton(typeof(PaymentOrderImportEventNotifier))]
    class PaymentOrderImportEventNotifier
    {
        private readonly ILogger logger;
        private readonly IMovementDocumentEventWriter importEventWriter;

        public PaymentOrderImportEventNotifier(
            ILogger<PaymentOrderImportEventNotifier> logger,
            IMovementDocumentEventWriter importEventWriter)
        {
            this.logger = logger;
            this.importEventWriter = importEventWriter;
        }

        public Task WriteCompletedAsync(int ImportId, string sourceFileId, long documentBaseId, int[] importRuleIds, int? importLogId)
        {
            var data = new { ImportId, sourceFileId, documentBaseId, importRuleIds };
            logger.LogInformation($"Import completed. {data.ToJsonString()}.");
            return importEventWriter.WriteDocumentImportCompletedAsync(
                new DocumentImportCompleted
                {
                    ImportId = ImportId,
                    SourceFileId = sourceFileId,
                    DocumentBaseId = documentBaseId,
                    ImportRuleIds = importRuleIds,
                    ImportLogId = importLogId
                });
        }

        public Task WriteSkippedAsync(int ImportId, string sourceFileId, object extraData = null, SkippedImportReason skippedData = null)
        {
            logger.LogWarning($"Import skipped. {extraData.ToJsonString()}.");
            return importEventWriter.WriteDocumentImportSkippedAsync(
                new DocumentImportSkipped
                {
                    ImportId = ImportId,
                    SourceFileId = sourceFileId,
                    IsInClosedPeriod = skippedData.IsInClosedPeriod,
                    ImportLogId = skippedData.ImportLogId
                });
        }

        public Task WriteFailedAsync(int ImportId, string sourceFileId, int? importLogId, Exception exception = null, object extraData = null)
        {
            if (exception != null)
            {
                logger.LogError(exception, $"Import exception. {extraData.ToJsonString()}.");
            }
            else
            {
                logger.LogError($"Import failed. {extraData.ToJsonString()}.");
            }
            return importEventWriter.WriteDocumentImportFailedAsync(
                new DocumentImportFailed
                {
                    ImportId = ImportId,
                    SourceFileId = sourceFileId,
                    ImportLogId = importLogId
                });
        }
    }
}
