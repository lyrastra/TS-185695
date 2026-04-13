using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Commands;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing;

public class RentPaymentCommandsHostedService : BackgroundService
{
    private readonly ILogger logger;
    private readonly IRentPaymentCommandReaderBuilder commandReaderBuilder;
    private readonly IRentPaymentImporter importer;

    public RentPaymentCommandsHostedService(
        ILogger<RentPaymentCommandsHostedService> logger,
        IRentPaymentCommandReaderBuilder commandReaderBuilder,
        IRentPaymentImporter importer)
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
            .OnImportWithMissingContract(OnImportWithMissingContractAsync)
            .SkipIfUnknownType()
            .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
            .WithAutoConsumersCount()
            .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
            .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
            .RunAsync(MoneyConstants.GroupId, cancellationToken);

        logger.LogHostedServiceIsStopped(GetType().Name);
    }

    private async Task OnImportAsync(ImportRentPayment commandData)
    {
        var request = RentPaymentMapper.Map(commandData);
        await importer.ImportAsync(request);
    }

    private async Task OnImportDuplicateAsync(ImportDuplicateRentPayment commandData)
    {
        var request = RentPaymentMapper.Map(commandData);
        await importer.ImportAsync(request);
    }

    private async Task OnImportWithMissingContractorAsync(ImportWithMissingContractorRentPayment commandData)
    {
        var request = RentPaymentMapper.Map(commandData);
        await importer.ImportAsync(request);
    }
    
    private async Task OnImportWithMissingContractAsync(ImportWithMissingContractRentPayment commandData)
    {
        var request = RentPaymentMapper.Map(commandData);
        await importer.ImportAsync(request);
    }
}