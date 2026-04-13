using System.Threading.Tasks;

namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.Events
{
    public interface IMovementEventWriter
    {
        Task WriteProcessedEventAsync(MovementProcessedEvent processedEvent);
    }
}