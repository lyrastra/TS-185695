using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;

namespace Moedelo.Money.Kafka.Abstractions.BankBalanceHistory.Events
{
    public class MovementProcessed : IEntityEventData
    {
        public int FirmId { get; set; }
        public int SettlementAccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
