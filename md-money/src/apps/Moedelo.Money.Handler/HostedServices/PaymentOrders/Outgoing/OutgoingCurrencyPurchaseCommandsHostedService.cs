using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class OutgoingCurrencyPurchaseCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IOutgoingCurrencyPurchaseCommandReaderBuilder commandReaderBuilder;
        private readonly IOutgoingCurrencyPurchaseImporter importer;

        public OutgoingCurrencyPurchaseCommandsHostedService(
            ILogger<OutgoingCurrencyPurchaseCommandsHostedService> logger,
            IOutgoingCurrencyPurchaseCommandReaderBuilder commandReaderBuilder,
            IOutgoingCurrencyPurchaseImporter importer)
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
                .OnImportWithMissingCurrencySettlementAccount(OnImportWithMissingCurrencySettlementAccountAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportOutgoingCurrencyPurchase commandData)
        {
            var request = OutgoingCurrencyPurchaseMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateOutgoingCurrencyPurchase commandData)
        {
            var request = OutgoingCurrencyPurchaseMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingExchangeRateAsync(
            ImportWithMissingMissingExchangeRateOutgoingCurrencyPurchase commandData)
        {
            var request = OutgoingCurrencyPurchaseMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingCurrencySettlementAccountAsync(
            ImportWithMissingCurrencySettlementAccountOutgoingCurrencyPurchase commandData)
        {
            var request = OutgoingCurrencyPurchaseMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
