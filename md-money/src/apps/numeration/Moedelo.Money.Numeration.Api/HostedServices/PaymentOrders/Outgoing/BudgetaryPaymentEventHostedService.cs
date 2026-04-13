using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.Numeration.Api.HostedServices.LoggerExtension;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.AspNetCore.Mvc.Attributes;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events;
using Moedelo.Money.Numeration.Business.Abstractions.PaymentOrders;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;

namespace Moedelo.Money.Numeration.Api.HostedServices.PaymentOrders.Outgoing;

[InjectAsHostedService]
public class BudgetaryPaymentEventHostedService : BackgroundService
{
    private readonly ILogger logger;
    private readonly IBudgetaryPaymentEventReaderBuilder eventReaderBuilder;
    private readonly INumberSetter numberSetter;

    public BudgetaryPaymentEventHostedService(
        ILogger<BudgetaryPaymentEventHostedService> logger,
        IBudgetaryPaymentEventReaderBuilder eventReaderBuilder,
        INumberSetter numberSetter)
    {
        this.logger = logger;
        this.eventReaderBuilder = eventReaderBuilder;
        this.numberSetter = numberSetter;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogHostedServiceIsStarting(GetType().Name);

        await eventReaderBuilder
            .OnCreated(OnCreated)
            .SkipIfUnknownType()
            .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
            .WithRetry(NumerationConstants.RetrySettings)
            .WithAutoConsumersCount()
            .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
            .RunAsync(NumerationConstants.GroupId, cancellationToken);

        logger.LogHostedServiceIsStopped(GetType().Name);
    }

    private async Task OnCreated(BudgetaryPaymentCreated eventData)
    {
        if (!int.TryParse(eventData.Number, out int lastNumber))
        {
            return;
        }
        await numberSetter.Last(eventData.SettlementAccountId, eventData.Date.Year, lastNumber);
    }
}