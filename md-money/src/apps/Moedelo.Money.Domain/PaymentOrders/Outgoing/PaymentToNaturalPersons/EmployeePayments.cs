using Moedelo.Money.Enums;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class EmployeePayments
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public TaxationSystemType TaxationSystem { get; set; }

        public decimal Sum { get; set; }

        public IReadOnlyCollection<ChargePayment> ChargePayments { get; set; }
    }
}
