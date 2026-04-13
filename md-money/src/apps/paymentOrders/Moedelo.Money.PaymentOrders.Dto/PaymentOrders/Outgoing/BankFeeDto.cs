using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing
{
    public class BankFeeDto
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }
        public int SettlementAccountId { get; set; }
        public string Description { get; set; }
        public TaxationSystemType? TaxationSystemType { get; set; }

        public string BankName { get; set; }

        public OperationState OperationState { get; set; }
        public string SourceFileId { get; set; }
        public long? DuplicateId { get; set; }
        public OutsourceState? OutsourceState { get; set; }

        public bool ProvideInAccounting { get; set; }
        public ProvidePostingType PostingsAndTaxMode { get; set; }
        public ProvidePostingType TaxPostingType { get; set; }
        public long? PatentId { get; set; }
    }
}
