using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class WithdrawalFromAccountCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IWithdrawalFromAccountCommandReaderBuilder commandReaderBuilder;
        private readonly IWithdrawalFromAccountImporter importer;
        private readonly IWithdrawalFromAccountIgnoreNumberSaver ignoreNumberSaver;

        public WithdrawalFromAccountCommandsHostedService(
            ILogger<WithdrawalFromAccountCommandsHostedService> logger,
            IWithdrawalFromAccountCommandReaderBuilder commandReaderBuilder,
            IWithdrawalFromAccountImporter importer,
            IWithdrawalFromAccountIgnoreNumberSaver ignoreNumberSaver)
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
                .OnApplyIgnoreNumber(OnApplyIgnoreNumberAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportWithdrawalFromAccount commandData)
        {
            var request = WithdrawalFromAccountMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateWithdrawalFromAccount commandData)
        {
            var request = WithdrawalFromAccountMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnApplyIgnoreNumberAsync(ApplyIgnoreNumberWithdrawalFromAccount commandData)
        {
            var request = WithdrawalFromAccountMapper.Map(commandData);
            return ignoreNumberSaver.ApplyIgnoreNumberAsync(request);
        }
    }
}
