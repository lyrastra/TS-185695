using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Events
{
    public class DeductionUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = new Contractor();

        public long? ContractBaseId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsPaid { get; set; }
        
        public bool IsBudgetaryDebt { get; set; }
        
        public PaymentPriority PaymentPriority { get; set; }
        
        public string Oktmo { get; set; }
        
        public string Kbk { get; set; }
        
        public string Uin { get; set; }
        
        public string DeductionWorkerDocumentNumber { get; set; }
        
        public int? DeductionWorkerId { get; set; }

        public string DeductionWorkerInn { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}