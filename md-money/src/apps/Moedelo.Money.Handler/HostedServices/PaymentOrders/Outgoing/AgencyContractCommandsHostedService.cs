using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class AgencyContractCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IAgencyContractCommandReaderBuilder commandReaderBuilder;
        private readonly IAgencyContractImporter importer;
        private readonly SettingValue consumerCountSetting;

        public AgencyContractCommandsHostedService(
            ILogger<AgencyContractCommandsHostedService> logger,
            IAgencyContractCommandReaderBuilder commandReaderBuilder,
            IAgencyContractImporter importer,
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
                .OnImportWithMissingContract(OnImportWithMissingContractAsync)
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnImportAsync(ImportAgencyContract commandData)
        {
            var request = AgencyContractMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportDuplicateAsync(ImportDuplicateAgencyContract commandData)
        {
            var request = AgencyContractMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportWithMissingContractAsync(ImportWithMissingContractAgencyContract commandData)
        {
            var request = AgencyContractMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportWithMissingContractorAsync(ImportWithMissingContractorAgencyContract commandData)
        {
            var request = AgencyContractMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }
    }
}
