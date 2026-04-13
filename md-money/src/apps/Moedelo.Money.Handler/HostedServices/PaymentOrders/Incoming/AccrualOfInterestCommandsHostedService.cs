using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Commands;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class AccrualOfInterestCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IAccrualOfInterestCommandReaderBuilder commandReaderBuilder;
        private readonly IAccrualOfInterestImporter importer;
        private readonly SettingValue consumerCountSetting;

        public AccrualOfInterestCommandsHostedService(
            ILogger<AccrualOfInterestCommandsHostedService> logger,
            IAccrualOfInterestCommandReaderBuilder commandReaderBuilder,
            IAccrualOfInterestImporter importer,
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

        private Task OnImportAsync(ImportAccrualOfInterest commandData)
        {
            var request = AccrualOfInterestMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateAccrualOfInterest commandData)
        {
            var request = AccrualOfInterestMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
