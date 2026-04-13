using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanRepayment.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class LoanRepaymentCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ILoanRepaymentCommandReaderBuilder commandReaderBuilder;
        private readonly ILoanRepaymentImporter importer;

        public LoanRepaymentCommandsHostedService(
            ILogger<LoanRepaymentCommandsHostedService> logger,
            ILoanRepaymentCommandReaderBuilder commandReaderBuilder,
            ILoanRepaymentImporter importer)
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
                .OnImportWithMissingContract(OnImportWithMissingContractAsync)
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnImportAsync(ImportLoanRepayment commandData)
        {
            var request = LoanRepaymentMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportDuplicateAsync(ImportDuplicateLoanRepayment commandData)
        {
            var request = LoanRepaymentMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportWithMissingContractAsync(ImportWithMissingContractLoanRepayment commandData)
        {
            var request = LoanRepaymentMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportWithMissingContractorAsync(ImportWithMissingContractorLoanRepayment commandData)
        {
            var request = LoanRepaymentMapper.Map(commandData);
            await importer.ImportAsync(request);
        }
    }
}
