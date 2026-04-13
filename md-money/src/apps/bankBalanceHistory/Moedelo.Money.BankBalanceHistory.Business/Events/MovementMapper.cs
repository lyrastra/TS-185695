using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Events;
using Moedelo.Money.Kafka.Abstractions.BankBalanceHistory.Events;

namespace Moedelo.Money.BankBalanceHistory.Business.Events
{
    internal static class MovementMapper
    {
        internal static MovementProcessed MapToMovementProcessed(MovementProcessedEvent processedEvent)
        {
            return new MovementProcessed
            {
                FirmId = processedEvent.FirmId,
                SettlementAccountId = processedEvent.SettlementAccountId,
                StartDate = processedEvent.StartDate,
                EndDate = processedEvent.EndDate,
            };
        }
    }
}
