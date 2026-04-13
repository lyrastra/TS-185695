using System;

namespace Moedelo.BankIntegrations.Dto.PaymentRegistries
{
    public class PaymentDto
    {
        public int Number { get; set; }
        public string AccountNumber { get; set; }
        public string PaymentPurpose { get; set; }
        public EmployeeInfoDto EmployeeInfo { get; set; }
        public decimal Sum { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}
