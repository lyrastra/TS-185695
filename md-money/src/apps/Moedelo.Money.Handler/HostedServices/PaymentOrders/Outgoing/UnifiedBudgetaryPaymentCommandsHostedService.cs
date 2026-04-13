using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class UnifiedBudgetaryPaymentCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IUnifiedBudgetaryPaymentCommandReaderBuilder commandReaderBuilder;
        private readonly IUnifiedBudgetaryPaymentImporter importer;

        public UnifiedBudgetaryPaymentCommandsHostedService(
            ILogger<UnifiedBudgetaryPaymentCommandsHostedService> logger,
            IUnifiedBudgetaryPaymentCommandReaderBuilder commandReaderBuilder,
            IUnifiedBudgetaryPaymentImporter importer)
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
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportUnifiedBudgetaryPayment commandData)
        {
            var request = UnifiedBudgetaryPaymentMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateUnifiedBudgetaryPayment commandData)
        {
            var request = UnifiedBudgetaryPaymentMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
