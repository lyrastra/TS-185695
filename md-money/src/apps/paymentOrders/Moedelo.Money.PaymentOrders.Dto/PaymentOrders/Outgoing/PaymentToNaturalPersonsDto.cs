using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing
{
    public class PaymentToNaturalPersonsDto
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public int SettlementAccountId { get; set; }
        public string Description { get; set; }

        public WorkerDto Worker { get; set; }
        public IReadOnlyCollection<WorkerChargePaymentDto> WorkerPayments { get; set; }
        public PaymentToNaturalPersonsType PaymentType { get; set; }

        public OperationState OperationState { get; set; }
        public string SourceFileId { get; set; }
        public long? DuplicateId { get; set; }
        public OutsourceState? OutsourceState { get; set; }

        public bool ProvideInAccounting { get; set; }
        public ProvidePostingType PostingsAndTaxMode { get; set; }

        public bool IsPaid { get; set; }
    }
}
