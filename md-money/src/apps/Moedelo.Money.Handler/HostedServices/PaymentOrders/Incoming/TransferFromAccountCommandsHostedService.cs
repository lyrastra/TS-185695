using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class TransferFromAccountCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ITransferFromAccountCommandReaderBuilder commandReaderBuilder;
        private readonly ITransferFromAccountImporter importer;
        private readonly SettingValue consumerCountSetting;

        public TransferFromAccountCommandsHostedService(
            ILogger<TransferFromAccountCommandsHostedService> logger,
            ITransferFromAccountCommandReaderBuilder commandReaderBuilder,
            ITransferFromAccountImporter importer,
            ISettingRepository settingRepository)
        {
            this.logger = logger;
            this.commandReaderBuilder = commandReaderBuilder;
            this.importer = importer;
            consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);

            await commandReaderBuilder
                .OnImport(OnImportAsync)
                .OnImportDuplicate(OnImportDuplicateAsync)
                .OnImportAmbiguousOperationType(OnImportAmbiguousOperationTypeAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnImportAsync(ImportTransferFromAccount commandData)
        {
            var request = TransferFromAccountMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportDuplicateAsync(ImportDuplicateTransferFromAccount commandData)
        {
            var request = TransferFromAccountMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportAmbiguousOperationTypeAsync(
            ImportAmbiguousOperationTypeTransferFromAccount commandData)
        {
            var request = TransferFromAccountMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }
    }
}
