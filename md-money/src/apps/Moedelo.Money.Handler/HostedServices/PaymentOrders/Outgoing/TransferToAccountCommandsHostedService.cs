using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class TransferToAccountCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ITransferToAccountCommandReaderBuilder commandReaderBuilder;
        private readonly ITransferToAccountImporter importer;
        private readonly ITransferToAccountIgnoreNumberSaver ignoreNumberSaver;

        public TransferToAccountCommandsHostedService(
            ILogger<TransferToAccountCommandsHostedService> logger,
            ITransferToAccountCommandReaderBuilder commandReaderBuilder,
            ITransferToAccountImporter importer,
            ITransferToAccountIgnoreNumberSaver ignoreNumberSaver)
        {
            this.logger = logger;
            this.commandReaderBuilder = commandReaderBuilder;
            this.importer = importer;
            this.ignoreNumberSaver = ignoreNumberSaver;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await commandReaderBuilder
                .OnImport(OnImportAsync)
                .OnImportDuplicate(OnImportDuplicateAsync)
                .OnApplyIgnoreNumber(OnApplyIgnoreNumberAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportTransferToAccount commandData)
        {
            var request = TransferToAccountMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateTransferToAccount commandData)
        {
            var request = TransferToAccountMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnApplyIgnoreNumberAsync(ApplyIgnoreNumberTransferToAccount commandData)
        {
            var request = TransferToAccountMapper.Map(commandData);
            return ignoreNumberSaver.ApplyIgnoreNumberAsync(request);
        }
    }
}
