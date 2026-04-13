using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class PaymentToAccountablePersonCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IPaymentToAccountablePersonCommandReaderBuilder commandReaderBuilder;
        private readonly IPaymentToAccountablePersonImporter importer;
        private readonly IPaymentToAccountablePersonIgnoreNumberSaver ignoreNumberSaver;

        public PaymentToAccountablePersonCommandsHostedService(
            ILogger<PaymentToAccountablePersonCommandsHostedService> logger,
            IPaymentToAccountablePersonCommandReaderBuilder commandReaderBuilder,
            IPaymentToAccountablePersonImporter importer,
            IPaymentToAccountablePersonIgnoreNumberSaver ignoreNumberSaver)
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
                .OnImportWithMissingEmployee(OnImportWithMissingEmployeeAsync)
                .OnApplyIgnoreNumber(OnApplyIgnoreNumberAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportPaymentToAccountablePerson commandData)
        {
            var request = PaymentToAccountablePersonMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicatePaymentToAccountablePerson commandData)
        {
            var request = PaymentToAccountablePersonMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingEmployeeAsync(ImportWithMissingEmployeePaymentToAccountablePerson commandData)
        {
            var request = PaymentToAccountablePersonMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnApplyIgnoreNumberAsync(ApplyIgnoreNumberPaymentToAccountablePerson commandData)
        {
            var request = PaymentToAccountablePersonMapper.Map(commandData);
            return ignoreNumberSaver.ApplyIgnoreNumberAsync(request);
        }
    }
}
