using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.Marketplace.Events;

namespace Moedelo.Billing.Kafka.Abstractions.Marketplace.Writers;

public interface IProlongationAttemptEventWriter
{
    Task WriteAsync(ProlongationAttemptEvent eventData);
}