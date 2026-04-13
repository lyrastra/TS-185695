using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class TransferFromPurseCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ITransferFromPurseCommandReaderBuilder commandReaderBuilder;
        private readonly ITransferFromPurseImporter importer;
        private readonly SettingValue consumerCountSetting;

        public TransferFromPurseCommandsHostedService(
            ILogger<TransferFromPurseCommandsHostedService> logger,
            ITransferFromPurseCommandReaderBuilder commandReaderBuilder,
            ITransferFromPurseImporter importer,
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
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnImportAsync(ImportTransferFromPurse commandData)
        {
            var request = TransferFromPurseMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportDuplicateAsync(ImportDuplicateTransferFromPurse commandData)
        {
            var request = TransferFromPurseMapper.Map(commandData);
            await importer.ImportAsync(request);
        }
    }
}
