using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee
{
    public class CurrencyBankFeeResponse : IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }
        
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }
        
        public decimal Sum { get; set; }

        public string Description { get; set; }

        public bool ProvideInAccounting { get; set; }

        public decimal TotalSum { get; set; }
        
        public OperationType OperationType => OperationType.CurrencyBankFee;

        public bool TaxPostingsInManualMode { get; set; }

        public bool IsReadOnly { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public bool IsFromImport { get; set; }
    }
}