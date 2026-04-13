using Moedelo.Common.Enums.Enums.Accounting;
using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class EmployeePayment
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public TaxationSystemType TaxationSystem { get; set; }

        public IReadOnlyCollection<ChargePayment> ChargePayments { get; set; }
    }
}
