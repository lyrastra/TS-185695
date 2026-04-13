using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class ContributionOfOwnFundsCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IContributionOfOwnFundsCommandReaderBuilder commandReaderBuilder;
        private readonly IContributionOfOwnFundsImporter importer;
        private readonly SettingValue consumerCountSetting;

        public ContributionOfOwnFundsCommandsHostedService(
            ILogger<ContributionOfOwnFundsCommandsHostedService> logger,
            IContributionOfOwnFundsCommandReaderBuilder commandReaderBuilder,
            IContributionOfOwnFundsImporter importer,
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

        private async Task OnImportAsync(ImportContributionOfOwnFunds commandData)
        {
            var request = ContributionOfOwnFundsMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportDuplicateAsync(ImportDuplicateContributionOfOwnFunds commandData)
        {
            var request = ContributionOfOwnFundsMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportAmbiguousOperationTypeAsync(ImportAmbiguousOperationTypeContributionOfOwnFunds commandData)
        {
            var request = ContributionOfOwnFundsMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }
    }
}
