using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Commands;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class OutgoingPaymentOrderDeductionCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IOutgoingPaymentOrderDeductionCommandReaderBuilder builder;
        private readonly IDeductionImporter importer;

        public OutgoingPaymentOrderDeductionCommandsHostedService(
            ILogger<OutgoingPaymentOrderDeductionCommandsHostedService> logger,
            IOutgoingPaymentOrderDeductionCommandReaderBuilder builder,
            IDeductionImporter importer)
        {
            this.logger = logger;
            this.builder = builder;
            this.importer = importer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await builder
                .OnImport(OnImportAsync)
                .OnImportDuplicate(OnImportDuplicateAsync)
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .OnImportWithMissingEmployee(OnImportWithMissingEmployeeAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, stoppingToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportDeduction commandData)
        {
            var request = DeductionMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDeductionDuplicate commandData)
        {
            var request = DeductionMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingContractorAsync(ImportDeductionWithMissingContractor commandData)
        {
            var request = DeductionMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingEmployeeAsync(ImportDeductionWithMissingEmployee commandData)
        {
            var request = DeductionMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
