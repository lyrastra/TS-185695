using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class PaymentToNaturalPersonsCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IPaymentToNaturalPersonsCommandReaderBuilder commandReaderBuilder;
        private readonly IPaymentToNaturalPersonsImporter importer;

        public PaymentToNaturalPersonsCommandsHostedService(
            ILogger<PaymentToNaturalPersonsCommandsHostedService> logger,
            IPaymentToNaturalPersonsCommandReaderBuilder commandReaderBuilder,
            IPaymentToNaturalPersonsImporter importer)
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
                .OnImportWithMissingEmployee(OnImportWithMissingEmployeeAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportPaymentToNaturalPersons commandData)
        {
            var request = PaymentToNaturalPersonsMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicatePaymentToNaturalPersons commandData)
        {
            var request = PaymentToNaturalPersonsMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingEmployeeAsync(ImportWithMissingEmployeePaymentToNaturalPersons commandData)
        {
            var request = PaymentToNaturalPersonsMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
