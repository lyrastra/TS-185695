using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.Other;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class OutgoingPaymentOrderOtherCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IOutgoingPaymentOrderOtherCommandReaderBuilder builder;
        private readonly IOtherOutgoingImporter importer;

        public OutgoingPaymentOrderOtherCommandsHostedService(ILogger<OutgoingPaymentOrderOtherCommandsHostedService> logger,
            IOutgoingPaymentOrderOtherCommandReaderBuilder builder,
            IOtherOutgoingImporter importer)
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
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, stoppingToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportOtherOutgoing commandData)
        {
            var request = OutgoingOtherMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportOtherOutgoingDuplicate commandData)
        {
            var request = OutgoingOtherMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        // todo: удалить команду и связанный код после релиза AP-10710 (встроено в ImportOtherOutgoing)
        private Task OnImportWithMissingContractorAsync(ImportOtherOutgoingWithMissingContragent commandData)
        {
            var request = OutgoingOtherMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
