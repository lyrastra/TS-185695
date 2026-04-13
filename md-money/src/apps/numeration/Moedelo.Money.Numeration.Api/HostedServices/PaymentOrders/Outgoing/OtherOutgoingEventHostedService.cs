using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.Numeration.Api.HostedServices.LoggerExtension;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.AspNetCore.Mvc.Attributes;
using Moedelo.Money.Numeration.Business.Abstractions.PaymentOrders;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Events;

namespace Moedelo.Money.Numeration.Api.HostedServices.PaymentOrders.Outgoing;

[InjectAsHostedService]
public class OtherOutgoingEventHostedService : BackgroundService
{
    private readonly ILogger logger;
    private readonly IOtherOutgoingEventReaderBuilder eventReaderBuilder;
    private readonly INumberSetter numberSetter;

    public OtherOutgoingEventHostedService(
        ILogger<OtherOutgoingEventHostedService> logger,
        IOtherOutgoingEventReaderBuilder eventReaderBuilder,
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

    private async Task OnCreated(OtherOutgoingCreated eventData)
    {
        if (!int.TryParse(eventData.Number, out int lastNumber))
        {
            return;
        }
        await numberSetter.Last(eventData.SettlementAccountId, eventData.Date.Year, lastNumber);
    }
}