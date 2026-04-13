using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class WithdrawalOfProfitCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IWithdrawalOfProfitCommandReaderBuilder commandReaderBuilder;
        private readonly IWithdrawalOfProfitImporter importer;
        private readonly IWithdrawalOfProfitIgnoreNumberSaver ignoreNumberSaver;

        public WithdrawalOfProfitCommandsHostedService(
            ILogger<WithdrawalOfProfitCommandsHostedService> logger,
            IWithdrawalOfProfitCommandReaderBuilder commandReaderBuilder,
            IWithdrawalOfProfitImporter importer,
            IWithdrawalOfProfitIgnoreNumberSaver ignoreNumberSaver)
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

        private Task OnImportAsync(ImportWithdrawalOfProfit commandData)
        {
            var request = WithdrawalOfProfitMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateWithdrawalOfProfit commandData)
        {
            var request = WithdrawalOfProfitMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingContractorAsync(ImportWithMissingContractorWithdrawalOfProfit commandData)
        {
            var request = WithdrawalOfProfitMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnApplyIgnoreNumberAsync(ApplyIgnoreNumberWithdrawalOfProfit commandData)
        {
            var request = WithdrawalOfProfitMapper.Map(commandData);
            return ignoreNumberSaver.ApplyIgnoreNumberAsync(request);
        }
    }
}
