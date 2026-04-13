using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing
{
    public class CurrencyBankFeeDto
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public decimal TotalSum { get; set; }

        public bool ProvideInAccounting { get; set; }

        public ProvidePostingType TaxPostingType { get; set; }

        public string SourceFileId { get; set; }
        public long? DuplicateId { get; set; }
        public OperationState OperationState { get; set; }
        public OutsourceState? OutsourceState { get; set; }
    }
}