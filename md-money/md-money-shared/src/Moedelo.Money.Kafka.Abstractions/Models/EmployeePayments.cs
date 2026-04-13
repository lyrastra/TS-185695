using Moedelo.Money.Enums;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.Models
{
    public class EmployeePayments
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public IReadOnlyCollection<ChargePayment> ChargePayments { get; set; }
    }
}
