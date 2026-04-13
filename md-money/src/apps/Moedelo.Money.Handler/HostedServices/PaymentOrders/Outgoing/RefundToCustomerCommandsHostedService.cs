using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class RefundToCustomerCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IRefundToCustomerCommandReaderBuilder commandReaderBuilder;
        private readonly IRefundToCustomerImporter importer;

        public RefundToCustomerCommandsHostedService(
            ILogger<RefundToCustomerCommandsHostedService> logger,
            IRefundToCustomerCommandReaderBuilder commandReaderBuilder,
            IRefundToCustomerImporter importer)
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
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnImportAsync(ImportRefundToCustomer commandData)
        {
            var request = RefundToCustomerMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportDuplicateAsync(ImportDuplicateRefundToCustomer commandData)
        {
            var request = RefundToCustomerMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportWithMissingContractorAsync(ImportWithMissingContractorRefundToCustomer commandData)
        {
            var request = RefundToCustomerMapper.Map(commandData);
            await importer.ImportAsync(request);
        }
    }
}
