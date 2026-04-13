using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class CurrencyTransferToAccountCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ICurrencyTransferToAccountCommandReaderBuilder commandReaderBuilder;
        private readonly ICurrencyTransferToAccountImporter importer;

        public CurrencyTransferToAccountCommandsHostedService(
            ILogger<CurrencyTransferToAccountCommandsHostedService> logger,
            ICurrencyTransferToAccountCommandReaderBuilder commandReaderBuilder,
            ICurrencyTransferToAccountImporter importer)
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
                .OnImportWithMissingCurrencySettlementAccount(OnImportWithMissingCurrencySettlementAccountAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportCurrencyTransferToAccount commandData)
        {
            var request = CurrencyTransferToAccountMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateCurrencyTransferToAccount commandData)
        {
            var request = CurrencyTransferToAccountMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingCurrencySettlementAccountAsync(
            ImportWithMissingCurrencySettlementAccountCurrencyTransferToAccount commandData)
        {
            var request = CurrencyTransferToAccountMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
