using System.Threading.Tasks;
using Moedelo.Billing.Kafka.NetFramework.Abstractions.Marketplace.Events;

namespace Moedelo.Billing.Kafka.NetFramework.Abstractions.Marketplace.Writers;

public interface IProlongationAttemptEventWriter
{
    Task WriteAsync(ProlongationAttemptEvent eventData);
}