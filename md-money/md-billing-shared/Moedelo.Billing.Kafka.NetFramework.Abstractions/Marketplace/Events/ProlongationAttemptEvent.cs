using Moedelo.Billing.Kafka.Common.Marketplace.Events;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Billing.Kafka.NetFramework.Abstractions.Marketplace.Events;

/// <summary>
/// Событие "Попытка продления"
/// </summary>
public class ProlongationAttemptEvent : ProlongationAttemptEventFields, IEntityEventData
{
}