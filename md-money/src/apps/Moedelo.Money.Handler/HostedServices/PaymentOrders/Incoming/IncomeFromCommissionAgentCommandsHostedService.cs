using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class IncomeFromCommissionAgentCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IIncomeFromCommissionAgentCommandReaderBuilder commandReaderBuilder;
        private readonly IIncomeFromCommissionAgentImporter importer;
        private readonly SettingValue consumerCountSetting;

        public IncomeFromCommissionAgentCommandsHostedService(
            ILogger<IncomeFromCommissionAgentCommandsHostedService> logger,
            IIncomeFromCommissionAgentCommandReaderBuilder commandReaderBuilder,
            IIncomeFromCommissionAgentImporter importer,
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
                .OnImportWithMissingContract(OnImportWithMissingContractAsync)
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, cancellationToken);
        
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportIncomeFromCommissionAgent commandData)
        {
            var request = IncomeFromCommissionAgentMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateIncomeFromCommissionAgent commandData)
        {
            var request = IncomeFromCommissionAgentMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingContractAsync(ImportWithMissingContractIncomeFromCommissionAgent commandData)
        {
            var request = IncomeFromCommissionAgentMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingContractorAsync(ImportWithMissingContractorIncomeFromCommissionAgent commandData)
        {
            var request = IncomeFromCommissionAgentMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
