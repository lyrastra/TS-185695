using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;
using System;
using Moedelo.Money.Domain.Payroll;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    public class RefundFromAccountablePersonResponse : IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Employee Employee { get; set; }

        public RemoteServiceResponse<AdvanceStatementLink> AdvanceStatement { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsReadOnly { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public bool IsFromImport { get; set; }
    }
}
