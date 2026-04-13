using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class PaymentToSupplierCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IPaymentToSupplierCommandReaderBuilder commandReaderBuilder;
        private readonly IPaymentToSupplierImporter importer;
        private readonly IPaymentToSupplierIgnoreNumberSaver ignoreNumberSaver;

        public PaymentToSupplierCommandsHostedService(
            ILogger<PaymentToSupplierCommandsHostedService> logger,
            IPaymentToSupplierCommandReaderBuilder commandReaderBuilder,
            IPaymentToSupplierImporter importer,
            IPaymentToSupplierIgnoreNumberSaver ignoreNumberSaver)
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
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .OnApplyIgnoreNumber(OnApplyIgnoreNumberAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportPaymentToSupplier commandData)
        {
            var request = PaymentToSupplierMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicatePaymentToSupplier commandData)
        {
            var request = PaymentToSupplierMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingContractorAsync(ImportWithMissingContractorPaymentToSupplier commandData)
        {
            var request = PaymentToSupplierMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnApplyIgnoreNumberAsync(ApplyIgnoreNumberPaymentToSupplier commandData)
        {
            var request = PaymentToSupplierMapper.Map(commandData);
            return ignoreNumberSaver.ApplyIgnoreNumberAsync(request);
        }
    }
}
