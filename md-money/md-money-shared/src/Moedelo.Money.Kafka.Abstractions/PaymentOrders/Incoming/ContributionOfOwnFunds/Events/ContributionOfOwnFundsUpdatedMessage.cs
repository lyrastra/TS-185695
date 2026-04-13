using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds.Events
{
    public class ContributionOfOwnFundsUpdatedMessage
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}
