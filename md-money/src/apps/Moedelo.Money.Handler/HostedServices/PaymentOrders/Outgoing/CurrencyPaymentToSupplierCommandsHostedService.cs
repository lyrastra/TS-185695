using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class CurrencyPaymentToSupplierCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ICurrencyPaymentToSupplierCommandReaderBuilder commandReaderBuilder;
        private readonly ICurrencyPaymentToSupplierImporter importer;

        public CurrencyPaymentToSupplierCommandsHostedService(
            ILogger<CurrencyPaymentToSupplierCommandsHostedService> logger,
            ICurrencyPaymentToSupplierCommandReaderBuilder commandReaderBuilder,
            ICurrencyPaymentToSupplierImporter importer)
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
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .OnImportWithMissingExchangeRate(OnImportWithMissingExchangeRateAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportCurrencyPaymentToSupplier commandData)
        {
            var request = CurrencyPaymentToSupplierMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateCurrencyPaymentToSupplier commandData)
        {
            var request = CurrencyPaymentToSupplierMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingContractorAsync(
            ImportWithMissingMissingContractorCurrencyPaymentToSupplier commandData)
        {
            var request = CurrencyPaymentToSupplierMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingExchangeRateAsync(
            ImportWithMissingMissingExchangeRateCurrencyPaymentToSupplier commandData)
        {
            var request = CurrencyPaymentToSupplierMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
