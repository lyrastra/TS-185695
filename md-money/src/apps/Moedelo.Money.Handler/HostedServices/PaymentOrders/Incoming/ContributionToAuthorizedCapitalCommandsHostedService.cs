using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class ContributionToAuthorizedCapitalCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IContributionToAuthorizedCapitalCommandReaderBuilder commandReaderBuilder;
        private readonly IContributionToAuthorizedCapitalImporter importer;
        private readonly SettingValue consumerCountSetting;

        public ContributionToAuthorizedCapitalCommandsHostedService(
            ILogger<ContributionToAuthorizedCapitalCommandsHostedService> logger,
            IContributionToAuthorizedCapitalCommandReaderBuilder commandReaderBuilder,
            IContributionToAuthorizedCapitalImporter importer,
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
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnImportAsync(ImportContributionToAuthorizedCapital commandData)
        {
            var request = ContributionToAuthorizedCapitalMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportDuplicateAsync(ImportDuplicateContributionToAuthorizedCapital commandData)
        {
            var request = ContributionToAuthorizedCapitalMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportWithMissingContractorAsync(ImportWithMissingContractorContributionToAuthorizedCapital commandData)
        {
            var request = ContributionToAuthorizedCapitalMapper.Map(commandData);
            await importer.ImportAsync(request);
        }
    }
}
