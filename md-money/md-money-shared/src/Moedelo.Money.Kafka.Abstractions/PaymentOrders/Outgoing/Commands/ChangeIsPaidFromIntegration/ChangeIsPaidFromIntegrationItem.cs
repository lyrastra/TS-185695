using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ChangeIsPaidFromIntegration
{
    public class ChangeIsPaidFromIntegrationItem : IEntityCommandData
    {
        public long DocumentBaseId { get; set; }
        
        public DateTime? Date { get; set; }
        
        public bool? IsPaid { get; set; }
        
        public string PaymentNumber { get; set; }
        
        public string PayerSettlementNumber { get; set; }
    }
}