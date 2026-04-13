using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class BudgetaryPaymentCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IBudgetaryPaymentCommandReaderBuilder commandReaderBuilder;
        private readonly IBudgetaryPaymentImporter importer;
        private readonly SettingValue consumerCountSetting;

        public BudgetaryPaymentCommandsHostedService(
            ILogger<BudgetaryPaymentCommandsHostedService> logger,
            IBudgetaryPaymentCommandReaderBuilder commandReaderBuilder,
            IBudgetaryPaymentImporter importer,
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
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportBudgetaryPayment commandData)
        {
            var request = BudgetaryPaymentMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateBudgetaryPayment commandData)
        {
            var request = BudgetaryPaymentMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
