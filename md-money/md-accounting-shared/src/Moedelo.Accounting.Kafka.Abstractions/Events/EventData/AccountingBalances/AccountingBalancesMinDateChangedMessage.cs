using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounting.Kafka.Abstractions.Events.EventData.AccountingBalances
{
    public class AccountingBalancesMinDateChangedMessage : IEntityEventData
    {
        public DateTime? Date { get; set; }
    }
}