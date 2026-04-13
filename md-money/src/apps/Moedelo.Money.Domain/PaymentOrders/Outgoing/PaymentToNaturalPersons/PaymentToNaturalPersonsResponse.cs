using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class PaymentToNaturalPersonsResponse : IActualizableReadResponse, IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public IReadOnlyCollection<EmployeePayments> EmployeePayments { get; set; }

        public PaymentToNaturalPersonsType PaymentType { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsPaid { get; set; }

        public bool IsReadOnly { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}
