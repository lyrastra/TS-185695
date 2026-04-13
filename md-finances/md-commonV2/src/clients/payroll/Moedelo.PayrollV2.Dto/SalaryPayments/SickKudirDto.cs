using System;

namespace Moedelo.PayrollV2.Dto.SalaryPayments
{
    public class SickKudirDto
    {
        public DateTime Date { get; set; }

        public string PaymentOrderNumber { get; set; }

        public decimal Sum { get; set; }

        public bool IsNdfl { get; set; }
    }
}
