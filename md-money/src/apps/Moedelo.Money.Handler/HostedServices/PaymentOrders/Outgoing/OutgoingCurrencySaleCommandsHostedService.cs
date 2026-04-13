using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class OutgoingCurrencySaleCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IOutgoingCurrencySaleCommandReaderBuilder commandReaderBuilder;
        private readonly IOutgoingCurrencySaleImporter importer;

        public OutgoingCurrencySaleCommandsHostedService(
            ILogger<OutgoingCurrencySaleCommandsHostedService> logger,
            IOutgoingCurrencySaleCommandReaderBuilder commandReaderBuilder,
            IOutgoingCurrencySaleImporter importer)
        {
            this.logger = logger;
            this.commandReaderBuilder = commandReaderBuilder;
            this.importer = importer;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await commandReaderBuilder
                .OnImport(OnImportAsync)
                .OnImportDuplicate(OnImportDuplicateAsync)
                .OnImportWithMissingExchangeRate(OnImportWithMissingExchangeRateAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportOutgoingCurrencySale commandData)
        {
            var request = OutgoingCurrencySaleMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateOutgoingCurrencySale commandData)
        {
            var request = OutgoingCurrencySaleMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingExchangeRateAsync(
            ImportWithMissingMissingExchangeRateOutgoingCurrencySale commandData)
        {
            var request = OutgoingCurrencySaleMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
