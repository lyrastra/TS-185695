using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    public class CurrencyTransferToAccountResponse : IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int SettlementAccountId { get; set; }

        public int? ToSettlementAccountId { get; set; }

        public string Description { get; set; }

        public bool IsReadOnly { get; set; }

        public long? DuplicateId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public bool IsFromImport { get; set; }
    }
}